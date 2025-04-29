using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVC.Core;
using MVC.Domain;
using MVC.Infra;
using MVC.Soft.Data;

internal class Program
{
    private static async Task Main(string[] args) // <-- Make Main async
    {
        var builder = WebApplication.CreateBuilder(args);
        var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContext")
                               ?? throw new InvalidOperationException("Connection string not found.");

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString), ServiceLifetime.Transient);

        builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<ApplicationDbContext>();

        builder.Services.AddControllersWithViews();

        builder.Services.AddTransient<DbContext, ApplicationDbContext>();
        builder.Services.AddTransient<IAppointmentsRepo, AppointmentsRepo>();
        builder.Services.AddTransient<IDiagnosesRepo, DiagnosesRepo>();
        builder.Services.AddTransient<IDoctorsRepo, DoctorsRepo>();
        builder.Services.AddTransient<IPatientsRepo, PatientsRepo>();

        builder.Services.AddTransient<DbInitializer>();
        builder.Services.AddSingleton<OpenAiService>();

        Services.init(builder.Services);

        var app = builder.Build();

        using (var scope = app.Services.CreateScope()) // <-- Create a service scope
        {
            var initializer = scope.ServiceProvider.GetRequiredService<DbInitializer>();
            await initializer.Initialize(100); // <-- Seed data before app.Run
        }

        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
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
        app.MapRazorPages()
            .WithStaticAssets();

        await app.RunAsync(); // <-- await here too
    }
}
