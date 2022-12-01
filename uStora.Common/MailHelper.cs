using System.Net.Mail;

namespace uStora.Common
{
    public static class MailHelper
    {
        public static bool SendMail(string toEmail, string subject, string content)
        {
            try
            {
                var host = ConfigHelper.GetByKey("SMTPHost");
                var port = int.Parse(ConfigHelper.GetByKey("SMTPPort"));
                var fromEmail = ConfigHelper.GetByKey("FromEmailAddress");
                var password = ConfigHelper.GetByKey("FromEmailPassword");
                var fromName = ConfigHelper.GetByKey("FromName");

                //var smtpClient = new SmtpClient(host, port)
                //{
                //    UseDefaultCredentials = false,
                //    Credentials = new System.Net.NetworkCredential(fromEmail, password),
                //    DeliveryMethod = SmtpDeliveryMethod.Network,
                //    EnableSsl = true,
                //    Timeout = 100000
                //};

                //var mail = new MailMessage
                //{
                //    Body = content,
                //    Subject = subject,
                //    From = new MailAddress(fromEmail, fromName)
                //};
                MailMessage mail = new MailMessage();
                //mail.To.Add(new MailAddress(toEmail));
                //mail.BodyEncoding = System.Text.Encoding.UTF8;
                //mail.IsBodyHtml = true;
                //mail.Priority = MailPriority.High;

              
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("huudiem2510@gmail.com");
                mail.To.Add(new MailAddress(toEmail));
                mail.Subject = subject;
                mail.Body = content;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("fromEmail", "password");
                SmtpServer.EnableSsl = true;



                SmtpServer.Send(mail);

                return true;
            }
            catch (SmtpException)
            {
                return false;
            }
        }
    }
}