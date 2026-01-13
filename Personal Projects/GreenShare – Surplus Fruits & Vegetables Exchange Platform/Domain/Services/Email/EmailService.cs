using Domain.Helper;
using Domain.Interfaces.Email;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Email
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

            //email.Sender = MailboxAddress.Parse(emailsettings.Email);
            email.From.Add(new MailboxAddress(
     mailRequest.FromName ?? emailsettings.DisplayName,
     emailsettings.Email
 ));
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
