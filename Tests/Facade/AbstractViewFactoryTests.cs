using MVC.Data;
using MVC.Facade;

namespace MVC.Tests.Facade
{
    [TestClass] public class AbstractViewFactoryTests
    : AbstractTests<AbstractViewFactory<PatientData, PatientView>, object>
    {
        protected override AbstractViewFactory<PatientData, PatientView> createObj()
            => new PatientViewFactory();
    }
}
