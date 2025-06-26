using Microsoft.EntityFrameworkCore;
using ModelagemAPI.Data;
using ModelagemAPI.Models;
using System.Text.Json.Serialization;
using ModelagemAPI.Services;

using System;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

// Cria o objeto de configuração da aplicação
var builder = WebApplication.CreateBuilder(args);

// Adiciona os controladores ao builder
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});

// Registra AlunoService como um serviço escopo ao builder
builder.Services.AddScoped<AlunoService>();
// Learn more about configuring Swagger/OpenApi at https://aka.ms/aspnetcore/swashbuckle

// Gera documentação interativa da API (interface Swagger).
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adiciona um BackgroundService que roda verificando se há provas a serem processadas.
builder.Services.AddHostedService<ChecaProvas>();


// Configure MySQL DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Após registrar todos os serviços, o app é criado.
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


// Redireciona para HTTPS e ativa controle de autorização (não tem autenticação aqui ainda).
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Endpoint /alunos-por-prova/{idProva}
app.MapGet("/alunos-por-prova/{idProva}", async (int idProva, AlunoService alunoService) =>
{
    // Chama AlunoService para buscar os alunos da prova com o ID fornecido.
    var alunos = await alunoService.GetAlunosByProvaId(idProva);

    // Retorna 404 se a prova não existir
    if (alunos == null)
    {
        return Results.NotFound($"Prova with ID {idProva} not found.");
    }

    // Retorna 404 se a turma dela não tiver alunos.
    if (!alunos.Any())
    {
        return Results.NotFound($"No students found for Prova with ID {idProva} or associated Turma.");
    }

    Console.WriteLine($"Alunos for Prova ID {idProva}:");

    foreach (var aluno in alunos)
    {
        Console.WriteLine($"- Aluno ID: {aluno.idAluno}");
    }

    // Serializa os alunos.
    var options = new System.Text.Json.JsonSerializerOptions
    {
        ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve
    };
    
    return Results.Json(alunos, options);
});

// Endpoint /avisos
app.MapGet("/avisos", async (AppDbContext context) =>
{
    // Busca as provas agendadas para hoje e para a próxima semana.
    var hoje = DateTime.Today;
    var umaSemana = hoje.AddDays(7);

    // Usa .Include() para carregar as chaves estrangeiras (disciplina_fk, tipo_fk, etc.).
    Prova[] provasDia = await context.Prova
        .Where(p => p.dataHora.Date == hoje)
        .Include(p => p.disciplina_fk)
        .Include(p => p.turma_fk)
        .Include(p => p.sala_fk)
        .Include(p => p.tipo_fk)
        .ToArrayAsync();
    
    Prova[] provasSemana = await context.Prova
        .Where(p => p.dataHora.Date == umaSemana)
        .Include(p => p.disciplina_fk)
        .Include(p => p.turma_fk)
        .Include(p => p.sala_fk)
        .Include(p => p.tipo_fk)
        .ToArrayAsync();

    // Se não houver provas agendadas para hoje ou para a próxima semana, retorna 404.
    if (provasDia.Length == 0 && provasSemana.Length == 0)
    {
        return Results.NotFound("Nenhuma prova agendada para hoje ou para a próxima semana.");
    }

    // Caso haja provas, cria uma instância de EmailService e chama o método EnviarAvisosAsync.
    // Isso enviará os e-mails para os alunos associados às provas.
    var emailService = new EmailService(context);
    await emailService.EnviarAvisosAsync(provasDia, provasSemana);

    return Results.Ok("OK");
});

// Método para enviar e-mail usando MailKit
void EnviarEmail(string para, string assunto, string corpo)
{
    // Pega usuario e senha do arquivo email.json
    var emailConfig = System.IO.File.ReadAllText("email.json");
    var emailSettings = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(emailConfig);
    var _email = emailSettings["email"];
    var _senha = emailSettings["senha"];

    Console.WriteLine($"{_email} - {_senha}");

    // Cria uma nova mensagem de e-mail
    // Define o remetente, destinatário, assunto e corpo do e-mail.
    var email = new MimeMessage();
    email.From.Add(new MailboxAddress("Tester", "inf1013testepuc@gmail.com")); // Remetente
    email.To.Add(MailboxAddress.Parse(para));
    email.Subject = assunto;
    email.Body = new TextPart("plain") { Text = corpo };
    SmtpClient smtp = new SmtpClient();
    smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls); // ou 465, com SecureSocketOptions.SslOnConnect
    smtp.Authenticate(_email, _senha);
    smtp.Send(email);
    smtp.Dispose();
}

// Endpoint /enviar-email para enviar e-mail com parametros
app.MapGet("/enviar-email", (string destinatario, string assunto, string mensagem) =>
{
    try
    {
        EnviarEmail("leogiuris@gmail.com", assunto, mensagem);
        return Results.Ok("E-mail enviado com sucesso!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro ao enviar e-mail: {ex.Message}");
        return Results.Problem("Erro ao enviar o e-mail: " + ex.Message);
    }
});

// Inicia o servidor web da aplicação (Kestrel).
app.Run();

