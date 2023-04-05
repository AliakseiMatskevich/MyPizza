using Microsoft.AspNetCore.Identity.UI.Services;
using MailKit.Security;
using MimeKit;
using MailKit.Net.Smtp;

namespace MyPizza.Web.Services.EmailSender
{
    public class EmailSender : IEmailSender
    {
        private readonly ILogger _logger;
        private readonly string _emailFrom;
        private readonly string _password;
        private readonly string _host;
        private readonly int _port;
        public EmailSender(ILogger<EmailSender> logger,
                           IConfiguration configuration)
        {
            _logger = logger;
            _emailFrom = configuration["EmailSettings:Email"]!;
            _password = configuration["EmailSettings:Password"]!;
            _host = configuration["EmailSettings:Host"]!;
            int.TryParse(configuration["EmailSettings:Port"]!, out _port);
        }

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            var email = new MimeMessage();

            email.From.Add(MailboxAddress.Parse(_emailFrom));
            email.To.Add(MailboxAddress.Parse(toEmail));
            email.Subject = subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = message };

            var smtpServer = new SmtpClient();

            smtpServer.Connect(_host, _port, SecureSocketOptions.StartTls);
            await smtpServer.AuthenticateAsync(_emailFrom, _password);
            await smtpServer.SendAsync(email);
            smtpServer.Disconnect(true);
            _logger.LogInformation($"Email to {toEmail} send successfully!");           
        }        
    }
}
