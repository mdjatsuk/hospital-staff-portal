using MVC.Data;

namespace MVC.Tests.Data
{
    [TestClass] public class GendersTests() : EnumTests<Genders>(2)
    {
        [TestMethod] public void MaleTest() => isEnum(0);
        [TestMethod] public void FemaleTest() => isEnum(1);
    }
}
