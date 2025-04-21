using Microsoft.EntityFrameworkCore;
using MVC_Project.Soft.Models;

namespace MVC_Project.Soft.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<MVC_Project.Domain.Patient> Patients { get; set; } = default!;

    public DbSet<MVC_Project.Domain.AppointmentData> AppointmentData { get; set; } = default!;



    public DbSet<MVC_Project.Domain.Doctor> Doctor { get; set; } = default!;

    public DbSet<MVC_Project.Domain.Diagnosis> Diagnosis { get; set; } = default!;


}