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

    // Esse método é chamado automaticamente quando o app inicia.
    // Ele roda em loop até o serviço ser cancelado (ex: quando a aplicação fecha).
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Verifica se o serviço foi cancelado, e se não, continua rodando.
        while (!stoppingToken.IsCancellationRequested)
        {
            // Cria um escopo de injeção de dependência para que serviços scoped como AppDbContext possam ser usados aqui corretamente.
            using (var scope = _serviceProvider.CreateScope())
            {
                // Obtém o contexto do banco de dados do escopo atual.  
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var hoje = DateTime.Today;
                var umaSemana = hoje.AddDays(7);

                // Busca todas as provas com as datas sendo hoje
                Prova[] provasDia = await context.Prova
                    .Where(p => p.dataHora.Date == hoje) // Data de hoje
                    .Include(p => p.disciplina_fk) // Inclui a disciplina relacionada
                    .Include(p => p.turma_fk)   // Inclui a turma relacionada
                    .Include(p => p.sala_fk)  // Inclui a sala relacionada
                    .Include(p => p.tipo_fk)  // Inclui o tipo de prova relacionado
                    .ToArrayAsync();
                
                // Busca todas as provas com as datas sendo hoje
                Prova[] provasSemana = await context.Prova
                    .Where(p => p.dataHora.Date == umaSemana)  // Data da próxima semana
                    .Include(p => p.disciplina_fk) // Inclui a disciplina relacionada
                    .Include(p => p.turma_fk)  // Inclui a turma relacionada
                    .Include(p => p.sala_fk)   // Inclui a sala relacionada
                    .Include(p => p.tipo_fk)   // Inclui o tipo de prova relacionado
                    .ToArrayAsync();

                // Instancia EmailService manualmente, passando o context.   
                var emailService = new EmailService(context);
                await emailService.EnviarAvisosAsync(provasDia, provasSemana);

            }
            // Espera 24 horas até a próxima execução.
            // Se a aplicação for encerrada, stoppingToken cancela a espera.
            await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
        }
    }
}