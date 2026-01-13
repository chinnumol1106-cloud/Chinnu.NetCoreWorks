using imageupload.Helper;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;


namespace imageupload.Service
{
    public class EmailService:IEmailService
    {

        private readonly EmailSettings emailsettings;
        public EmailService(IOptions<EmailSettings> options)
        {
            this.emailsettings = options.Value;
        }
        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(emailsettings.Email);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();
            var smtp = new SmtpClient();
            smtp.Connect(emailsettings.Host, emailsettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(emailsettings.Email, emailsettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

    }
}
