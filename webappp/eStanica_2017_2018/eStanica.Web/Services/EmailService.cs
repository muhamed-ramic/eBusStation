using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace eStanica.Web.Services
{
    public interface IEmailService
    {
        /// <summary>
        /// Prepares template for sending email
        /// </summary>
        /// <param name="subjectText"></param>
        /// <param name="bodytext"></param>
        /// <param name="email"></param>
        void BuildEmailTemplate(string subjectText, string bodytext, string email);

        /// <summary>
        /// Sends email using mail client.
        /// </summary>
        /// <param name="email"></param>
        void SendEmail(MailMessage email);

    }
    public class EmailService : IEmailService
    {
        public void BuildEmailTemplate(string subjectText, string bodytext, string email)
        {
            string from, to, bcc="", cc="", subject, body;
            from = "bhopkelta@gmail.com";
            to = email.Trim();
            subject = subjectText;

            StringBuilder sb = new StringBuilder();
            sb.Append(bodytext);

            body = sb.ToString();
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(from);
            mail.To.Add(new MailAddress(to));

            if (!string.IsNullOrEmpty(bcc))
            {
                mail.Bcc.Add(new MailAddress(bcc));
            }

            if (!string.IsNullOrEmpty(cc))
            {
                mail.CC.Add(new MailAddress(cc));
            }
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            SendEmail(mail);
        }

        public void SendEmail(MailMessage email)
        {
            //Initialize stmp client
            SmtpClient klijent = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new System.Net.NetworkCredential("bhopkelta@gmail.com", "bjpucyrjzoiueueh")
            };

            try
            {
                klijent.Send(email);
            }
            catch (Exception x)
            {
                throw x;
            }
        }
    }
}
