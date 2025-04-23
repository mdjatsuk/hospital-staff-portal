using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVC.Data;

namespace MVC.Soft.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<PatientData> Patients { get; set; } = default!;

    public DbSet<AppointmentData> Appointments { get; set; } = default!;
    public DbSet<DoctorData> Doctors { get; set; } = default!;
    public DbSet<DiagnosisData> Diagnoses { get; set; } = default!;
}