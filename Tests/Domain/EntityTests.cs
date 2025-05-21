using Microsoft.VisualStudio.TestTools.UnitTesting;
using MVC.Core;
using MVC.Data;
using MVC.Domain;
using static MVC.Tests.Domain.EntityTests;

namespace MVC.Tests.Domain;

[TestClass] public class EntityTests : SealedTests<TestEntity, Entity<TestData>>
{
    protected override TestEntity createObj() => new TestEntity();
    [TestMethod] public void IdTest() => equal(42, obj?.Id);

    [TestMethod] public void DataTest() => notNull(obj?.data);

    [TestMethod] public async Task LoadLazyTest()
    {
        await obj!.LoadLazy();
        isTrue(true);
    }
}
public sealed class TestEntity : Entity<TestData>
{
    public TestEntity() : base(new TestData { Id = 42 }) {}
    public TestEntity(TestData? d) : base(d) {}
}
public class TestData : EntityData<TestData> {}
