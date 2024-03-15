using System.Net.Mail;

namespace Lesson_07_EmailSending
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("sendingmails@ukr.net");
                mail.To.Add("sendingmails@ukr.net");
                mail.Subject = "Test Mail - 1";
                mail.Body = "Test";
                //mail.Attachments.Add(new Attachment(path));

                using (SmtpClient smtpClient = new SmtpClient("smtp.ukr.net"))
                {
                    smtpClient.Port = 587;
                    smtpClient.Credentials = new System.Net.NetworkCredential("sendingmails@ukr.net", "MIFchiPSaOnitay3");
                    smtpClient.EnableSsl = false;

                    smtpClient.Send(mail);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
