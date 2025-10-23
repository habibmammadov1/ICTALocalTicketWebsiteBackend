using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helper.Mail
{
    public class MailHelper
    {
        private readonly string smtpHost = "smtp.gmail.com"; // SMTP server
        private readonly int smtpPort = 587; // TLS port
        private readonly string smtpUser = "turalmammadov2024@gmail.com"; // Your email
        private readonly string smtpPass = "eualcwdectrpdhcq"; // App password (Gmail)

        public void SendVerificationEmail(string toEmail, string subject, string body)
        {
            using (var client = new SmtpClient(smtpHost, smtpPort))
            {
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(smtpUser, smtpPass);

                var mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(smtpUser, "ICTA");
                mailMessage.To.Add(toEmail);
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;

                client.Send(mailMessage);
            }
        }
    }
}
