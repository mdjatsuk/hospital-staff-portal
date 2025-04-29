using Mvc.Aids.GoF.Crea;

namespace MVC.Tests.Aids.GoF.Creational
{
    [TestClass] public sealed class SingletonTests : BaseTests
    {
        protected override Type setType() => typeof(Singleton);

        [TestMethod] public void NewTest() => same(Singleton.New, Singleton.New);
    }
}
