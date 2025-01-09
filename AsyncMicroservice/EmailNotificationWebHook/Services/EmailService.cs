using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Shared.DTOs;

namespace EmailNotificationWebHook.Services
{
    public class EmailService : IEmailService
    {
        public string SendEmail(EmailDTO emailDTO)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(""));
            email.To.Add(MailboxAddress.Parse(""));
            email.Subject = emailDTO.Title;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) 
                { Text = emailDTO.Content };
            var smtp = new SmtpClient();
            smtp.Connect("", 44355, SecureSocketOptions.StartTls);
            smtp.Authenticate("", "", CancellationToken.None);
            smtp.Send(email);
            smtp.Disconnect(true);
            return "Email sent!";
        }
    }
}
