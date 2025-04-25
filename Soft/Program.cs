using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MVC.Core;
using MVC.Domain;
using MVC.Infra;
using MVC.Soft.Data;
using Mvc.Soft.Data;

internal class Program
{
    private static void Main(string[] args)
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

        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
            seedData(app);
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
        app.Run();
    }

    private static void seedData(WebApplication app)
    {
        Task.Run(async () => {
            IServiceProvider? services = null;
            try
            {
                using var scope = app.Services.CreateScope();
                services = scope.ServiceProvider;
                var initializer = services.GetRequiredService<DbInitializer>();
                await initializer.Initialize(1000, 250);
            }
            catch (Exception e)
            {
                var logger = services?.GetRequiredService<ILogger<Program>>();
                logger?.LogError(e, "An error occurred while seeding the database.");
            }
        });
    }
}



