using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using MVC.Soft.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Tests.Soft.Services;

[TestClass] public class EmailSenderTests : BaseTests
{
    protected override Type setType() => typeof(EmailSender);
    private Mock<ILogger<EmailSender>> _loggerMock;
    private Mock<IConfiguration> _configMock;
    [TestInitialize] public void Setup()
    {
        _loggerMock = new Mock<ILogger<EmailSender>>();
        _configMock = new Mock<IConfiguration>();
    }
    [TestMethod] public async Task SendEmailAsyncTest()
    {
        _configMock.Setup(c => c["AuthMessageSenderOptions:SendGridKey"]).Returns("dummy");
        var sender = new EmailSender(_configMock.Object, _loggerMock.Object);
        try
        {
            await sender.SendEmailAsync("test@example.com", "subject", "body");
        }
        catch (Exception ex)
        {
            isTrue(ex.Message.Contains("401") || ex.Message.Contains("invalid"), "Unexpected exception: " + ex.Message);
        }
    }
    [TestMethod] public async Task ExecuteTest()
    {
        _configMock.Setup(c => c["AuthMessageSenderOptions:SendGridKey"]).Returns("dummy");
        var sender = new EmailSender(_configMock.Object, _loggerMock.Object);
        try
        {
            await sender.Execute("dummy", "subject", "body", "test@example.com");
        }
        catch (Exception ex)
        {
            isTrue(ex.Message.Contains("401") || ex.Message.Contains("invalid"), "Unexpected exception: " + ex.Message);
        }
    }
}
