using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using MVC.Data;
using MVC.Domain;
using MVC.Facade;
using MVC.Infra;
using MVC.Soft.Controllers;
using MVC.Soft.Data;

namespace MVC.Tests.Soft.Controllers;

[TestClass] public class DoctorsControllerTests() :
ControllerBaseTests<DoctorsController, Doctor, DoctorData, DoctorView>
{
    protected override Doctor? createEntity(Func<DoctorData> getData)
       => new(getData());
    protected override DoctorsController createObj() => new(dbContext!);
    [TestMethod] public void SelectItemsTest()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "DoctorsTestDb")
            .Options;
        using var context = new ApplicationDbContext(options);
        context.Doctors.AddRange(
            new DoctorData { Id = 1, FirstName = "Alice", LastName = "Brown" },
            new DoctorData { Id = 2, FirstName = "Bob", LastName = "Smith" }
        );
        context.SaveChanges();
        var controller = new DoctorsController(context);
        var result = controller.SelectItems("", "2") as JsonResult;
        notNull(result);
        var items = ((IEnumerable<dynamic>)result.Value).ToList();
        equal(0, items.Count);
    }
}
