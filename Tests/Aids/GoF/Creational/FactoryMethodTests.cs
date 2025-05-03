using MVC.Aids.GoF.Creational;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Tests.Aids.GoF.Creational;

[TestClass] public sealed class FactoryMethodTests : BaseTests
{
    protected override Type setType() => typeof(FactoryMethod);

    [TestMethod] public void CreateTest()
    {
        var x = new CopyTestClass1() { Id = 1001, Name = "Aaa Bbb Ccc", ValidFrom = DateTime.Now};
        var y = FactoryMethod.Create<CopyTestClass2,CopyTestClass1>(x);
        isType(y, typeof(CopyTestClass2));
        equal("Aaa Bbb Ccc", y.Name);
        isNull(y.Id);
    }
}