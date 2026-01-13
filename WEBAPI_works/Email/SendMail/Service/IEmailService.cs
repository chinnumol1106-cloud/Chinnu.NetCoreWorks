using SendMail.Helper;

namespace SendMail.Service
{
    public interface IEmailService
    {
        Task SendEmailAsync(MailRequest mailrequest);
    }
}
