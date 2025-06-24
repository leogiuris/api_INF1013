using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using ModelagemAPI.Data;
using Microsoft.EntityFrameworkCore;
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
                Console.WriteLine($"Data de hoje: {hoje}");
                bool existe = await context.Prova
                    .AnyAsync(p => p.dataHora.Date == hoje, stoppingToken);
                if (existe)
                {
                    Console.WriteLine($"Exams found for today: {hoje}");
                }
                else
                {
                    Console.WriteLine($"No exams scheduled for today: {hoje}");
                }
            }
            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }
}