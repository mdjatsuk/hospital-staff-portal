using SendGrid.Helpers.Mail;
using SendGrid;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace MVC.Soft.Services;

public class EmailSender : IEmailSender
{
    private readonly ILogger _logger;
    private readonly string _apiKey;

    public EmailSender(IConfiguration config,
                       ILogger<EmailSender> logger)
    {
        _apiKey = config["AuthMessageSenderOptions:SendGridKey"]
            ?? throw new Exception("SendGrid API key not found.");
        _logger = logger;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string message)
    {
        if (string.IsNullOrEmpty(_apiKey))
        {
            throw new Exception("Null SendGridKey");
        }
        await Execute(_apiKey, subject, message, toEmail);
    }

    public async Task Execute(string apiKey, string subject, string message, string toEmail)
    {
        var client = new SendGridClient(apiKey);
        var msg = new SendGridMessage()
        {
            From = new EmailAddress("hospitalstaffportalnoreply@gmail.com", "Hospital Staff Portal"),
            Subject = subject,
            PlainTextContent = message,
            HtmlContent = message
        };
        msg.AddTo(new EmailAddress(toEmail));

        // Disable click tracking.
        // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
        //msg.SetClickTracking(false, false);
        var response = await client.SendEmailAsync(msg);
        _logger.LogInformation(response.IsSuccessStatusCode
                               ? $"Email to {toEmail} queued successfully!"
                               : $"Failure Email to {toEmail}");
    }
}
