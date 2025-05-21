using MVC.Aids.Attributes;
using MVC.Soft.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MVC.Tests.Soft.Data;

[TestClass] public class EmailGeneratorTests : BaseTests
{
    protected override Type setType() => typeof(EmailGenerator);
    private static System.Collections.Generic.IReadOnlyList<string> Domains =>
        EmailDomainProvider.EmailDomains;
    [TestMethod] public void GenerateTest()
    {
        var email = EmailGenerator.Generate("John", "Doe");
        var parts = email.Split('@');
        equal(2, parts.Length);
        equal("john.doe", parts[0]);
        CollectionAssert.Contains(Domains.ToList(), parts[1]);
    }
    [DataRow(null, "Doe")]
    [DataRow("John", null)]
    [DataRow("", "")]
    [DataRow("   ", "   ")]
    [TestMethod] public void GenerateTest1(string name, string surname)
    {
        var email = EmailGenerator.Generate(name, surname);
        AssertUserFormat(email);
    }
    private void AssertUserFormat(string email)
    {
        var match = Regex.Match(email, @"^user\d{4}@(.+)$");
        isTrue(match.Success, "Email does not match user####@domain format");
        var domain = match.Groups[1].Value;
        CollectionAssert.Contains(Domains.ToList(), domain);
    }
}
