
using System.Net;
using System.Net.Mail;

namespace EmailSend.MiddleWare
{
    public class Email : EmailSending
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            var myEmail = "piyushmaurya7798@gmail.com";
            var pass = "qbposjoyllyywcld";

            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(myEmail, pass)
            };

            return client.SendMailAsync(
                new MailMessage(from: myEmail,
                to: email,
                subject,
                message
                )
                );
        }
    }
}
