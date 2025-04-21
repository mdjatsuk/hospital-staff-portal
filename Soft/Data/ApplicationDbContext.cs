using Microsoft.EntityFrameworkCore;
using MVC.Domain;
using MVC.Soft.Models;

namespace MVC.Soft.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Patient> Patients { get; set; } = default!;

    public DbSet<AppointmentData> AppointmentData { get; set; } = default!;
    public DbSet<Doctor> Doctors { get; set; } = default!;
    public DbSet<Diagnosis> Diagnoses { get; set; } = default!;
}