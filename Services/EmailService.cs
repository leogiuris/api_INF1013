using ModelagemAPI.Data;
using Microsoft.EntityFrameworkCore;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using ModelagemAPI.Models;

// Esse código implementa um serviço de envio de avisos por e-mail para alunos com base em provas agendadas,
// usando Entity Framework Core para acessar o banco de dados e MailKit/MimeKit para enviar e-mails.

namespace ModelagemAPI.Services
{
    // Define um serviço que envia e-mails e registra avisos no banco de dados.
    public class EmailService
    {

        private readonly AppDbContext _context;
        public EmailService(AppDbContext context)
        {
            _context = context;
        }

        public async Task EnviarAvisosAsync(Prova[] provasDia, Prova[] provasSemana)
        {
            // Junta as provas do dia e da semana em um único array.
            Prova[] provas_all = provasDia.Concat(provasSemana).ToArray();
            // Para cada prova...
            foreach (var prova in provas_all)
            {
                // Busca os alunos associados a essa prova, filtrando por turmas que tenham essa prova.
                // Isso garante que apenas os alunos que realmente têm essa prova sejam notificados.
                var alunos = await _context.Aluno
                    // Dentro do contexto da tabela alunos, Verifica se alguma turma satisfaz a condição de ter uma prova com o ID da prova atual, e pega os alunos dessa turma.
                    .Where(a => a.turmas.Any(t => t.provas.Any(p => p.idProva == prova.idProva)))
                    .ToListAsync();

                // Para cada aluno encontrado, cria um aviso e envia um e-mail.
                foreach (var aluno in alunos)
                {
                    int _tipoAviso;
                    string _mensagem;

                    // Verifica se a prova está no array de provas do dia.
                    // Se estiver, define o tipo de aviso como 0 (aviso imediato).
                    // Caso contrário, define como 1 (aviso para a próxima semana).
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

                    // Cria um novo aviso com os detalhes da prova e do aluno.
                    // Define o tipo de aviso, aluno, prova, data de envio, data a enviar, mensagem, turma e disciplina.
                    // Em seguida, adiciona o aviso ao contexto do banco de dados e salva as alterações
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




        // Método privado para enviar um e-mail usando o MailKit.
        // Recebe o destinatário, assunto e corpo do e-mail como parâmetros.
        private void EnviarEmail(string para, string assunto, string corpo)
        {
            // Pega usuario e senha do arquivo email.json
            var emailConfig = System.IO.File.ReadAllText("email.json");
            var emailSettings = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(emailConfig);
            var _email = emailSettings["email"];
            var _senha = emailSettings["senha"];

            Console.WriteLine($"{_email} - {_senha}");


            // Cria uma nova mensagem de e-mail usando o MimeKit.
            var email = new MimeMessage();
            // Define o remetente, destinatário, assunto e corpo do e-mail. 
            email.From.Add(new MailboxAddress("Tester", "inf1013testepuc@gmail.com")); // Remetente
            // Adiciona o destinatário (para) ao e-mail.
            email.To.Add(MailboxAddress.Parse(para));
            // Define o assunto do e-mail.
            // O assunto é definido como um texto simples.
            email.Subject = assunto;
            // Define o corpo do e-mail como um texto simples.
            // O corpo é passado como um parâmetro para o método.
            email.Body = new TextPart("plain") { Text = corpo };
            // Cria um cliente SMTP para enviar o e-mail.
            // O cliente SMTP é configurado para se conectar ao servidor SMTP do Gmail.
            SmtpClient smtp = new SmtpClient();
            // Conecta ao servidor SMTP do Gmail usando TLS.
            // O servidor SMTP do Gmail usa a porta 587 para conexões TLS (Transport Layer Security).
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls); // ou 465, com SecureSocketOptions.SslOnConnect
            // Autentica o cliente SMTP usando o e-mail e a senha fornecidos.
            // Isso é necessário para enviar e-mails através do servidor SMTP do Gmail.
            smtp.Authenticate(_email, _senha);
            // Envia o e-mail usando o cliente SMTP.
            smtp.Send(email);
            // Desconecta o cliente SMTP do servidor.
            smtp.Dispose();
        }





    }
}