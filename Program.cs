using Microsoft.EntityFrameworkCore;
using ModelagemAPI.Data;
using ModelagemAPI.Models;
using System.Text.Json.Serialization;
using ModelagemAPI.Services;
using System;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});
builder.Services.AddScoped<AlunoService>();
// Learn more about configuring Swagger/OpenApi at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddHostedService<ChecaProvas>();


// Configure MySQL DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/alunos-por-prova/{idProva}", async (int idProva, AlunoService alunoService) =>
{
    var alunos = await alunoService.GetAlunosByProvaId(idProva);

    if (alunos == null)
    {
        return Results.NotFound($"Prova with ID {idProva} not found.");
    }

    if (!alunos.Any())
    {
        return Results.NotFound($"No students found for Prova with ID {idProva} or associated Turma.");
    }

    Console.WriteLine($"Alunos for Prova ID {idProva}:");
    foreach (var aluno in alunos)
    {
        Console.WriteLine($"- Aluno ID: {aluno.idAluno}");
    }

    var options = new System.Text.Json.JsonSerializerOptions
    {
        ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve
    };
    
    return Results.Json(alunos, options);
});

app.MapGet("/hello", () =>
{
    Console.WriteLine("Hello from new endpoint! (console)");
    return "Hello from new endpoint! (response)";
});





void EnviarEmail(string para, string assunto, string corpo)
{
    // Pega usuario e senha do arquivo email.json
    var emailConfig = System.IO.File.ReadAllText("email.json");
    var emailSettings = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(emailConfig);
    var _email = emailSettings["email"];
    var _senha = emailSettings["senha"];

    Console.WriteLine($"{_email} - {_senha}");

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

// Endpoint para enviar e-mail com parametros
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

app.Run();

