using MVC.Data;

namespace MVC.Tests.Data;

[TestClass] public class GendersTests() : EnumTests<Genders>(3)
{
    [TestMethod] public void UnknownTest() => isEnum(0);
    [TestMethod] public void MaleTest() => isEnum(1);
    [TestMethod] public void FemaleTest() => isEnum(2);
}