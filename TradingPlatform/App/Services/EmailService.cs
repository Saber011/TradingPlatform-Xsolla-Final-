using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System.Threading.Tasks;
using TradingPlatform.App.Interfaces;

namespace TradingPlatform.App.Services
{
    /// <inheritdoc/>
    public sealed class EmailService : IEmailService
    {
        private IConfiguration Configuration { get; }

        public EmailService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <inheritdoc/>
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Trading platform", Configuration["Email"]));
            emailMessage.To.Add(new MailboxAddress("Доброго времени суток", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using var client = new SmtpClient();
            await client.ConnectAsync("smtp.mail.ru", 465, true);
            await client.AuthenticateAsync(Configuration["Email"], Configuration["Password"]);
            await client.SendAsync(emailMessage);

            await client.DisconnectAsync(true);
        }
    }
}
