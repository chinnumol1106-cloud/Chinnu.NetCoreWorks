using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SendMail.Helper;
using SendMail.Service;

namespace SendMail.Controllers
{
    [Route("api[controller]")]
    [ApiController]
    public class CustomController : ControllerBase
    {
        private readonly IEmailService _emailService;
        public CustomController(IEmailService emailService) 
        {
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> SendMail()
        {
            try
            {
                MailRequest mailrequest = new MailRequest();
                mailrequest.ToEmail = "jerilchinnu21@gmail.com";
                mailrequest.Subject = "Interview Updation";
                mailrequest.Body = "We are pleased to inform you that you have been shortlisted for the position of Backend-Developer Intern at Ericcson.";
                await _emailService.SendEmailAsync(mailrequest);
                return Ok();

            }
            catch (Exception ex) 
            {
                throw;
            }
        }
    }
}
