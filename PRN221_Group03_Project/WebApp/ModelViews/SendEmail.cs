using System.Net.Mail;
using System.Net;

namespace WebApp.ModelViews
{
    public class SendEmail
    {
        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587, // Port SMTP server,
                Credentials = new NetworkCredential("luan251103@gmail.com", "zfuo oevk uqqw bael"),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("luan251103@gmail.com"),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(toEmail);

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
