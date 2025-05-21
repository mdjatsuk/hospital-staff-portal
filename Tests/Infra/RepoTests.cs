using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.Data;
using MVC.Domain;
using MVC.Infra;

namespace MVC.Tests.Infra;

[TestClass] public class RepoTests
    : RepoBaseTests<Repo<Testing, TestingData>, Testing, TestingData>
{
    protected override Testing? createEntity(Func<TestingData> getData)
        => new(getData());

    protected override Repo<Testing, TestingData> createObj()
        => new Repo<Testing, TestingData>(dbContext!, d => new Testing(d));

    [TestMethod] public override void IsSealedTest() =>
        isFalse(typeof(Repo<Testing, TestingData>).IsSealed);

    [TestMethod] public void IsAbstractTest() =>
        isFalse(typeof(Repo<Testing, TestingData>).IsAbstract);

    [TestMethod] public override void IsBaseTypeOfTest() =>
        equal(typeof(Repo<Testing, TestingData>).BaseType, typeof(object));
    [TestMethod] public async Task SelectItemsTest()
    {
        var repo = createObj();
        var testData = new TestingData { Id = 123 };
        dbContext!.Add(testData);
        dbContext.SaveChanges();
        var result = await repo.SelectItems("", 123);
        notNull(result, "SelectItems should not return null.");
        isType(result, typeof(SelectList), "Result should be a SelectList.");
        var selectList = (SelectList)result;
        var items = selectList.ToList();
        isTrue(items.Count > 0, "SelectList should contain at least one item.");
        var selectedItem = items.FirstOrDefault(i => i.Value == "123");
        notNull(selectedItem, "SelectList should contain the item with Id 123.");
        isTrue(selectedItem.Selected, "The item with Id 123 should be selected.");
    }
}
