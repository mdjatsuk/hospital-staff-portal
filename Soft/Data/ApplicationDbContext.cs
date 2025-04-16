using Microsoft.EntityFrameworkCore;

namespace MVC_Project.Soft.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<MVC_Project.Domain.Patient> Patients { get; set; } = default!;
}