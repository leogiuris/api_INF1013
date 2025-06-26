using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using ModelagemAPI.Data;
using Microsoft.EntityFrameworkCore;
using ModelagemAPI.Models;
using ModelagemAPI.Services;
using Microsoft.Extensions.DependencyInjection;

public class ChecaProvas : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public ChecaProvas(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var hoje = DateTime.Today;
                var umaSemana = hoje.AddDays(7);
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

                

                var emailService = new EmailService(context);
                await emailService.EnviarAvisosAsync(provasDia, provasSemana);

            }
            await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
        }
    }
}