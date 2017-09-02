using System;
using System.Net;
using System.Net.Mail;
using Unicorn.Core.Interfaces;
using Unicorn.Shared.DTOs.Email;

namespace Unicorn.Core.Services
{
    public class MailService : IMailService
    {
        private readonly string Login;
        private readonly string Password;
        private readonly EmailHost EmailHost;

        public MailService()
        {
            Login = Properties.Settings.Default.EmailLogin;
            Password = Properties.Settings.Default.EmailPassword;
            EmailHost = new EmailHost
            {
                Host = Properties.Settings.Default.EmailSMTP,
                Port = Properties.Settings.Default.EmailPort,
                SSL = Properties.Settings.Default.EmailSSL
            };
        }

        private SmtpClient GetSmtpClient()
        {
            return new SmtpClient
            {
                Host = EmailHost.Host,
                Port = EmailHost.Port,
                EnableSsl = EmailHost.SSL,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential
                {
                    UserName = Login,
                    Password = Password
                }
            };
        }

        public void Send(EmailMessage msg)
        {
            var smtp = GetSmtpClient();

            using (var message = new MailMessage(Login, msg.ReceiverEmail))
            {
                message.Subject = msg.Subject;
                message.Body = msg.Body;
                message.IsBodyHtml = msg.IsHtml;
                smtp.Send(message);
            }
        }
    }
}
