using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVC.Data;

namespace MVC.Soft.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<PatientData> Patients { get; set; } = default!;
    public DbSet<DoctorData> Doctors { get; set; } = default!;
    public DbSet<MedicineData> Medicines { get; set; } = default!;
    public DbSet<AppointmentData> Appointments { get; set; } = default!;
    public DbSet<MedicalRecordData> MedicalRecords { get; set; } = default!;
}