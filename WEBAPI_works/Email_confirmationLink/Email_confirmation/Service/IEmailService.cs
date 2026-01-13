using Email_confirmation.Helper;

namespace Email_confirmation.Service
{
    public interface IEmailService
    {
        Task SendEmailAsync(MailRequest mailrequest);
    }
}
