using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MVC.Core;
using MVC.Domain;
using Mvc.Infra;
using MVC.Soft.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationDbContext") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContext' not found.")));

builder.Services.AddControllersWithViews();

builder.Services.AddTransient<DbContext, ApplicationDbContext>();
builder.Services.AddTransient<IAppointmentsRepo, AppointmentsRepo>();
builder.Services.AddTransient<IDiagnosesRepo, DiagnosesRepo>();
builder.Services.AddTransient<IDoctorsRepo, DoctorsRepo>();
builder.Services.AddTransient<IPatientsRepo, PatientsRepo>();

Services.init(builder.Services);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
