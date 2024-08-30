using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace EShop.Utilities;

public class EmailSender : IEmailSender
{
    public string SendGridSecret { get; set; }

    public EmailSender(IConfiguration _config)
    {
        SendGridSecret = _config.GetValue<string>("SendGrid:SecretKey");
    }

    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        if (SendGridSecret == "SENDGRID_API_KEY_PLACEHOLDER")
        {
            return Task.CompletedTask;
        }

        var client = new SendGridClient(SendGridSecret);
        var from = new EmailAddress("info@kordis.cloud", "info");
        var to = new EmailAddress(email);
        var message = MailHelper.CreateSingleEmail(from, to, subject, "", htmlMessage);

        return client.SendEmailAsync(message);
    }
}

