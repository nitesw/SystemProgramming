using MailKit.Net.Imap;
using MimeKit;
using System.IO;
using System.Net.Mail;
using MailKit.Net.Smtp;
using MailKit;

namespace Lesson_07_EmailSending
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string host = "imap.ukr.net";
            int port = 993;
            try
            {
                MimeMessage mail = new MimeMessage();
                mail.From.Add(new MailboxAddress("SearchForWord", "sendingmails@ukr.net"));
                mail.To.Add(new MailboxAddress("Receiver", "sendingmails@ukr.net"));
                mail.Subject = "Results";

                var bodyBuilder = new BodyBuilder();
                bodyBuilder.TextBody = "Some info which I found: ";
                mail.Body = bodyBuilder.ToMessageBody();

                using (var client = new ImapClient())
                {
                    await client.ConnectAsync(host, port, true);

                    await client.AuthenticateAsync("sendingmails@ukr.net", "r3LkTddNciFBAwoq");

                    var folder = await client.GetFolderAsync("INBOX");

                    await folder.AppendAsync(mail);

                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
