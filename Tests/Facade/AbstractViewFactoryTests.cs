using MVC.Data;
using MVC.Facade;

namespace MVC.Tests.Facade;

[TestClass] public class AbstractViewFactoryTests
    : AbstractTests<AbstractViewFactory<PatientData, PatientView>, object>
{
    protected override AbstractViewFactory<PatientData, PatientView> createObj()
        => new PatientViewFactory();
    [TestMethod] public void CreateViewTest()
    {
        var factory = createObj();
        var data = new PatientData { Id = 1 };
        var view = factory.CreateView(data);
        notNull(view);
        equal(data.Id, view.Id);
    }
    [TestMethod] public async Task CreateViewTest1()
    {
        var factory = createObj();
        var data = new PatientData { Id = 2 };
        var view = await factory.CreateView(data, true);
        notNull(view);
        equal(data.Id, view.Id);
    }
    [TestMethod] public void CreateDataTest()
    {
        var factory = createObj();
        var view = new PatientView { Id = 3 };
        var data = factory.CreateData(view);
        notNull(data);
        equal(view.Id, data.Id);
    }
}