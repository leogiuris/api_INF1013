using Microsoft.EntityFrameworkCore;
using ModelagemAPI.Data;
using ModelagemAPI.Models;
using System.Text.Json.Serialization;
using ModelagemAPI.Services;

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

app.MapGet("/hello", () => {
    Console.WriteLine("Hello from new endpoint! (console)");
    return "Hello from new endpoint! (response)";
});

app.Run();

