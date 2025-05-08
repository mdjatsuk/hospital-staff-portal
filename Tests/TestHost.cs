using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MVC.Core;
using MVC.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Random = MVC.Aids.Random;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Tests
{
    internal class TestHost<TPrg, TDb> : WebApplicationFactory<TPrg>
    where TPrg : class where TDb : DbContext
    {
        private TDb? db;
        protected override void ConfigureWebHost(IWebHostBuilder b) => b.UseEnvironment("Testing")
            .ConfigureServices(s => {
                removeDatabase<TDb>(s);
                s.AddEntityFrameworkInMemoryDatabase();
                addInMemoryDatabase<TDb>(s);
                startHost<TPrg>(s);
            });
        private void removeDatabase<T>(IServiceCollection c) where T : DbContext
        {
            var d = c.SingleOrDefault(d => d.ServiceType == typeof(IDbContextOptionsConfiguration<T>));
            if (d != null) { c.Remove(d); }
        }
        private void addInMemoryDatabase<T>(IServiceCollection c) where T : DbContext
            => c.AddDbContext<T>(o => o.UseInMemoryDatabase(Guid.NewGuid().ToString()));
        private void startHost<T>(IServiceCollection c)
            => c.AddControllersWithViews(o => o.Filters
                .Add(new IgnoreAntiforgeryTokenAttribute()))
                .AddApplicationPart(typeof(T).Assembly);
        internal TDb? StartDb()
        {
            var scope = Services.CreateScope();
            db = scope.ServiceProvider.GetRequiredService<TDb>();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            return db;
        }
        internal HttpClient? StartClient() => CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
        internal byte lastId;
        internal byte nextId => ++lastId;
        internal TData CreateData<TData>() where TData : EntityData<TData>, new()
        {
            var d = Random.Object<TData>();
            d.Id = nextId;
            return d;
        }
        internal void SeedData<TData>() where TData : EntityData<TData>, new()
        {
            var set = db?.Set<TData>();
            var l = new List<TData>();
            for (var i = 0; i < Random.UInt8(20, 30); i++) l.Add(CreateData<TData>());
            set?.AddRange(l);
            db?.SaveChanges();
        }
        internal void AddToSet<TData>(TData d1) where TData : EntityData<TData>, new()
        {
            var set = db?.Set<TData>();
            set?.Add(d1);
            db?.SaveChanges();
            db?.ChangeTracker.Clear();
        }
    }
}
