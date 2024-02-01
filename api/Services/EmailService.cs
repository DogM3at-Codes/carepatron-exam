using api.Models;
using MailKit.Net.Smtp;
using MimeKit;

namespace api.Services
{
    public sealed class EmailService : IEmailService
    {
        public async Task<Task> SendEmail(Client client)
        {
            try
            {
                var newMail = NewMailMessage(client);

                using (var smtp = new SmtpClient())
                {
                    smtp.Connect("sandbox.smtp.mailtrap.io", 587, false);
                    smtp.Authenticate("0c0da9d21b811e", "e58f33c274cb8e"); // hide key 
                    await smtp.SendAsync(newMail);
                    await smtp.DisconnectAsync(true);
                }

                return Task.CompletedTask;
            }
            catch (Exception)
            {
                // add logging 
                throw;
            }
        }

        private static MimeMessage NewMailMessage(Client client)
        {
            var newMail = new MimeMessage();

            newMail.From.Add(new MailboxAddress("CarePatron Mailer", "carepatronsupport@gmail.com"));
            newMail.To.Add(new MailboxAddress("CarePatron Mailer", client.Email));

            newMail.Subject = "New CarePatron User";
            newMail.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = String.Format("<h1>User for {0} created. This is an automated message do not reply.</h1>", client.FirstName + " " + client.LastName)
            };

            return newMail;
        }
    }
}
