using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using DotNetEnv;
class EmailService
{
    private readonly string SenderEmail;
    private readonly string SenderEmailPassword;

    public EmailService()
    {
        SenderEmail = Env.GetString("SENDER_EMAIL");
        SenderEmailPassword = Env.GetString("SENDER_EMAIL_PASSWORD");
    }

    public async Task<bool> SendEmail(SendEmailParams param)
    {
        var message = new MimeMessage();

        try
        {
            message.From.Add(new MailboxAddress("CoinTracker", SenderEmail));
            message.To.Add(new MailboxAddress(param.RecipientName, param.RecipientEmail));
            message.Subject = param.Subject;
            message.Body = new TextPart("plain") { Text = param.Body };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.zoho.com", 587, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(SenderEmail, SenderEmailPassword);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Email not sent!");
            Console.WriteLine(ex);
            return false;
        }
        finally
        {
            message.Dispose();
        }
    }
}
