using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using MVC.Core;
using MVC.Domain;
using MVC.Infra;
using MVC.Soft.Data;
using MVC.Soft.Services;

internal class Program
{
    private static async Task Main(string[] args)
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
        builder.Services.AddTransient<IDiagnosisRepo, DiagnosisRepo>();
        builder.Services.AddTransient<IDoctorsRepo, DoctorsRepo>();
        builder.Services.AddTransient<IPatientsRepo, PatientsRepo>();
        builder.Services.AddTransient<IMedicalRecordsRepo, MedicalRecordsRepo>();

        builder.Services.AddTransient<DbInitializer>();
        builder.Services.AddSingleton<OpenAiService>();

        builder.Services.AddTransient<IEmailSender, EmailSender>();
        builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration);

        builder.Services.AddAuthentication().AddGoogle(options =>
        {
            options.ClientId = builder.Configuration["Authentication:Google:ClientId"]
                               ?? throw new InvalidOperationException("Google ClientId is not configured.");
            options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"]
                                   ?? throw new InvalidOperationException("Google ClientSecret is not configured.");
        });


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

        await app.RunAsync();
    }

    private static void seedData(WebApplication app)
    {
        Task.Run(async () => {
            IServiceProvider? services = null;
            try
            {
                using var scope = app.Services.CreateScope();
                services = scope.ServiceProvider;
                var initializer = scope.ServiceProvider.GetRequiredService<DbInitializer>();
                await initializer.Initialize(10);
            }
            catch (Exception e)
            {
                var logger = services?.GetRequiredService<ILogger<Program>>();
                logger?.LogError(e, "An error occurred while seeding the database.");
            }
        });
    }
}
