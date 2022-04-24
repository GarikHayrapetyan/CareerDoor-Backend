using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Application.Core
{
    public static class EmailSender
    {
        public static IConfiguration Configuration;
        public static async Task SendEmailAsync(string toEmail, string subject, string htmlMessage) {

            string fromEmail = Configuration["EmailCredentials:Email"];
            string fromPassword = Configuration["EmailCredentials:Password"]; ;

            MailMessage message = new MailMessage(fromEmail,toEmail);
            message.Subject = subject;
            message.Body = "<html><body> " + htmlMessage + " </body></html>";
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                Credentials = new NetworkCredential(fromEmail, fromPassword),
                EnableSsl = true
                
            };

            smtpClient.Send(message);
        }
    }
}
