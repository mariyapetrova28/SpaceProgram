using SpaceProgram.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace SpaceProgram
{
    internal class EmailSender
    {
        public EmailSender() { }
        public void SendEmail(string senderEmail, string senderEmailPassword,string receiverEmail)
        {
            using SmtpClient email = new SmtpClient
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                EnableSsl = true,
                Host = "smtp.gmail.com",
                Port = 587,
                Credentials = new NetworkCredential(senderEmail, senderEmailPassword)
            };

            string subject = $"{LanguageHelper.GetString("wr")}";
            string body = $"{LanguageHelper.GetString("fileWR")}";
            string fileName = "WeatherReport.csv";
            string csvPath= Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);


            try
            {
                Console.WriteLine($"{LanguageHelper.GetString("Sending")}");

                // Create the attachment
                Attachment attachment = new Attachment(csvPath, MediaTypeNames.Application.Octet);

                // Create the email message and add the attachment
                using MailMessage mail = new MailMessage(senderEmail, receiverEmail, subject, body);
                mail.Attachments.Add(attachment);

                // Send the email
                email.Send(mail);

                Console.WriteLine($"{LanguageHelper.GetString("Sent")}");
                Console.WriteLine($"{LanguageHelper.GetString("GoodLuck")}");
            }
            catch (SmtpException ex)
            {
                Console.WriteLine($"{LanguageHelper.GetString("EmailNotSend")}\n{LanguageHelper.GetString("Correct")}");
                Console.WriteLine($"{LanguageHelper.GetString("AU")}\n{LanguageHelper.GetString("twoFactor")}");
            }
            catch(ArgumentException e)
            {
                Console.WriteLine($"{LanguageHelper.GetString("EmailNotSend")}\n{LanguageHelper.GetString("Correct")}");
                Console.WriteLine($"{LanguageHelper.GetString("noData")}");
            }
        }
        public void SendEmailDE(string senderEmail, string senderEmailPassword, string receiverEmail)
        {
            using SmtpClient email = new SmtpClient
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                EnableSsl = true,
                Host = "smtp.gmail.com",
                Port = 587,
                Credentials = new NetworkCredential(senderEmail, senderEmailPassword)
            };

            string subject = $"{LanguageHelper.GetString("wr")}";
            string body = $"{LanguageHelper.GetString("fileWR")}";
            string fileName = "Wetterbericht.csv";
            string csvPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);


            try
            {
                Console.WriteLine($"{LanguageHelper.GetString("Sending")}");

                // Create the attachment
                Attachment attachment = new Attachment(csvPath, MediaTypeNames.Application.Octet);

                // Create the email message and add the attachment
                using MailMessage mail = new MailMessage(senderEmail, receiverEmail, subject, body);
                mail.Attachments.Add(attachment);

                // Send the email
                email.Send(mail);

                Console.WriteLine($"{LanguageHelper.GetString("Sent")}");
                Console.WriteLine($"{LanguageHelper.GetString("GoodLuck")}");
            }
            catch (SmtpException ex)
            {
                Console.WriteLine($"{LanguageHelper.GetString("EmailNotSend")}\n{LanguageHelper.GetString("Correct")}");
                Console.WriteLine($"{LanguageHelper.GetString("AU")}\n{LanguageHelper.GetString("twoFactor")}");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine($"{LanguageHelper.GetString("EmailNotSend")}\n{LanguageHelper.GetString("Correct")}");
                Console.WriteLine($"{LanguageHelper.GetString("noData")}");
            }
        }
    }
}
