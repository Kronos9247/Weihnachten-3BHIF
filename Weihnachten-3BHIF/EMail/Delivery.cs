using System;
using System.Net;
using System.Net.Mail;

using Weihnachten.Members;

namespace Weihnachten.EMail
{
    public class Delivery : IDisposable
    {
        private SmtpClient client;
        private MailAddress sender;

        public string Email { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public bool Ready { get; protected set; }

        public Delivery()
        { }

        public void Start()
        {
            if (Ready)
                return;

            string password = ReadLine.ReadPassword("Email password: ");

            sender = new MailAddress(Email, Name);
            // https://myaccount.google.com/u/2/lesssecureapps
            client = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(sender.Address, password),
                Timeout = 20000
            };

            Ready = true;
        }

        public void SendTo(Student student, string[] values)
        {
            string to = $"{student.Name} {student.Surname}";
            string body = Content.Replace("{TO}", to);
            for (int i = 0; i < values.Length; i++)
            {
                body = body.Replace($"{{{i}}}", values[i]);
            }


            MailAddress recipient = new MailAddress(student.Email, to);
            using (MailMessage message = new MailMessage(sender, recipient)
            {
                Subject = Subject,
                Body = body,

                IsBodyHtml = true
            })
            {
                client.Send(message);
            }
        }

        public void Dispose()
        {
            if (!Ready)
                return;
        }
    }
}
