using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.Data;
using MVC.Domain;
using MVC.Facade;
using MVC.Soft.Controllers;

namespace MVC.Tests.Soft.Controllers;

[TestClass] public class BaseControllerTests() :
ControllerBaseTests<BaseController<Doctor, DoctorData, DoctorView>, Doctor, DoctorData, DoctorView>
{
    protected override BaseController<Doctor, DoctorData, DoctorView> createObj() => new DoctorsController(dbContext!);
    [TestMethod]
    public override void IsSealedTest() =>
        isFalse(typeof(BaseController<Doctor, DoctorData, DoctorView>).IsSealed);
    [TestMethod]
    public void IsAbstractTest() =>
        isTrue(typeof(BaseController<Doctor, DoctorData, DoctorView>).IsAbstract);
    [TestMethod]
    public override void IsBaseTypeOfTest() =>
        equal(typeof(BaseController<Doctor, DoctorData, DoctorView>).BaseType, typeof(Controller));
    protected override Doctor? createEntity(Func<DoctorData> getData) => new(getData());
    [TestMethod] public async Task SelectItemsTest()
    {
        var controller = createObj();
        var doctorData = new DoctorData
        {
            FirstName = "John",
            LastName = "Smith",
            Specialization = Specialities.Cardiology,
            PhoneNumber = 51234567,
            EmailAddress = "john.smith@example.com"
        };
        addToSet(doctorData);
        await dbContext!.SaveChangesAsync();
        var expectedId = doctorData.Id.ToString();
        var expectedText = $"{doctorData.FirstName} {doctorData.LastName}";
        var result = await controller.SelectItems("Smith", doctorData.Id);
        notNull(result);
        var okResult = result as OkObjectResult;
        notNull(okResult);
        var items = ((IEnumerable<SelectListItem>)okResult.Value!).ToList();
        isTrue(items.Count > 0);
        var item = items.FirstOrDefault(i => i.Value == expectedId);
        notNull(item, $"No item found with Value == {expectedId}");
        equal(expectedId, item.Value);
        equal(expectedId, item.Text);
        isTrue(item.Selected);
    }
}
