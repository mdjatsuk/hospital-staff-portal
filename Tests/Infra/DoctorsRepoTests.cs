using MVC.Data;
using MVC.Domain;
using MVC.Infra;

namespace MVC.Tests.Infra;

[TestClass] public class DoctorsRepoTests
   : RepoBaseTests<DoctorsRepo, Doctor, DoctorData>
{
    protected override Doctor? createEntity(Func<DoctorData> getData)
        => new(getData());
    protected override DoctorsRepo createObj() => new(dbContext!);
    [TestMethod] public void GetDoctorsTest()
    {
        dbContext!.Set<DoctorData>().RemoveRange(dbContext.Set<DoctorData>());
        dbContext.SaveChanges();
        var repo = createObj();
        var doctor1 = new Doctor(new DoctorData { FirstName = "Alice", LastName = "Smith" });
        var doctor2 = new Doctor(new DoctorData { FirstName = "Bob", LastName = "Jones" });
        var doctor3 = new Doctor(new DoctorData { FirstName = "Alicia", LastName = "Brown" });
        dbContext.Set<DoctorData>().Add(doctor1.data!);
        dbContext.Set<DoctorData>().Add(doctor2.data!);
        dbContext.Set<DoctorData>().Add(doctor3.data!);
        dbContext.SaveChanges();
        var resultAli = repo.GetDoctors("Ali").ToList();
        isTrue(resultAli.Any(d => d.FirstName == "Alice"), "Should find Alice");
        isTrue(resultAli.Any(d => d.FirstName == "Alicia"), "Should find Alicia");
        isFalse(resultAli.Any(d => d.FirstName == "Bob"), "Should not find Bob");
        var resultSmith = repo.GetDoctors("Smith").ToList();
        isTrue(resultSmith.Any(d => d.LastName == "Smith"), "Should find Smith");
        isFalse(resultSmith.Any(d => d.LastName == "Jones"), "Should not find Jones");
        var resultAll = repo.GetDoctors("").ToList();
        equal(3, resultAll.Count, "Should return all doctors");
        var resultNone = repo.GetDoctors("NonExistent").ToList();
        equal(0, resultNone.Count, "Should return no doctors");
    }
}
