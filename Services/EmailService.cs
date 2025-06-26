using ModelagemAPI.Data;
using Microsoft.EntityFrameworkCore;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using ModelagemAPI.Models;

namespace ModelagemAPI.Services
{
    public class EmailService
    {

        private readonly AppDbContext _context;
        public EmailService(AppDbContext context)
        {
            _context = context;
        }

        public async Task EnviarAvisosAsync(Prova[] provasDia, Prova[] provasSemana)
        {
            Prova[] provas_all = provasDia.Concat(provasSemana).ToArray();
            foreach (var prova in provas_all)
            {
                var alunos = await _context.Aluno
                    .Where(a => a.turmas.Any(t => t.provas.Any(p => p.idProva == prova.idProva)))
                    .ToListAsync();

                foreach (var aluno in alunos)
                {
                    int _tipoAviso;
                    string _mensagem;
                    if (provasDia.Contains(prova))
                    {
                        _tipoAviso = 0;
                        _mensagem = $"Prova Hoje: {prova.tipo_fk.tipo} de {prova.disciplina_fk.nome} "+
                        $"hoje na sala {prova.sala_fk.bloco}{prova.sala_fk.numero}.";
                    }
                    else
                    {
                        _tipoAviso = 1;
                        _mensagem = $"Prova semana que vem: No dia {prova.dataHora}, "+
                                    $"você tem {prova.tipo_fk.tipo} de {prova.disciplina_fk.nome} na sala "+
                                    $"{prova.sala_fk.bloco}{prova.sala_fk.numero}.";
                    }
                    var aviso = new Aviso
                    {   
                        tipoAviso = _tipoAviso,
                        aluno_fk = aluno,
                        prova_fk = prova,
                        dataEnviado = DateTime.Now,
                        dataAEnviar = DateTime.Now.AddDays(1),
                        mensagem = _mensagem,
                        turma_fk = prova.turma_fk,
                        disciplina_fk = prova.disciplina_fk
                    };

                    _context.Aviso.Add(aviso);
                    await _context.SaveChangesAsync();

                    EnviarEmail(aluno.email, "Lembrete de Prova", aviso.mensagem);
                }
            }
        }





        private void EnviarEmail(string para, string assunto, string corpo)
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





    }
}