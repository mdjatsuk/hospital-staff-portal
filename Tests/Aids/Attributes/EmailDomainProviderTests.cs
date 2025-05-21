using MVC.Aids.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Tests.Aids.Attributes;

[TestClass] public class EmailDomainProviderTests : BaseTests
{
    protected override Type setType() => typeof(EmailDomainProvider);
    [TestMethod] public void ListIsNotEmpty() => isTrue(EmailDomainProvider.EmailDomains.Count > 0);
    [TestMethod] public void ContainsCommonDomains()
    {
        var domains = EmailDomainProvider.EmailDomains;
        CollectionAssert.Contains(domains, "gmail.com");
        CollectionAssert.Contains(domains, "yahoo.com");
        CollectionAssert.Contains(domains, "outlook.com");
    }
    [TestMethod] public void DoesNotContainInvalidDomains()
    {
        var domains = EmailDomainProvider.EmailDomains;
        CollectionAssert.DoesNotContain(domains, "notrealmail.com");
    }
    [TestMethod] public void EmailDomainsTest()
    {
        ListIsNotEmpty();
        ContainsCommonDomains();
        DoesNotContainInvalidDomains();
    }
}