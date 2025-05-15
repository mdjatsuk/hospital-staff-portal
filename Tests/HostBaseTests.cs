using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using MVC.Data;
using MVC.Domain;
using MVC.Soft.Data;

namespace MVC.Tests;

public abstract class HostBaseTests<TClass, TBaseClass, TObject, TData> :
        BaseClassTests<TClass, TBaseClass>
    where TClass : class
    where TBaseClass : class
    where TObject : Entity<TData>
    where TData : EntityData<TData>, new()
{
    internal TestHost<Program, ApplicationDbContext>? host;
    protected ApplicationDbContext? dbContext;
    protected DbSet<TData>? dbSet;
    protected TObject? entity;
    protected abstract TObject? createEntity(Func<TData> getData);
    protected TData createData() => host!.CreateData<TData>();
    protected TObject? createEntity() => entity = createEntity(createData);
    [TestInitialize] public override void Initialize()
    {
        host = new TestHost<Program, ApplicationDbContext>();
        dbContext = host.StartDb();
        dbSet = dbContext!.Set<TData>();
        host.SeedData<TData>();
        base.Initialize();
    }
    [TestCleanup] public override void Cleanup()
    {
        base.Cleanup();
        dbContext = null;
        dbSet = null;
        entity = null;
    }
    [TestMethod] public virtual void IsSealedTest() => isTrue(typeof(TClass).IsSealed);
    [TestMethod] public void CanCreateDbContextTest() => notNull(dbContext);
    [TestMethod] public void HasDbSetTest() => notNull(dbSet);
    [TestMethod] public void DbSetHasDataTest() => isTrue(dbSet!.Any());
}
