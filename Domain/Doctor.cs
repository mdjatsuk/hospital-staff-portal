using MVC.Core;
using MVC.Data;

namespace MVC.Domain;

public sealed class Doctor(DoctorData? d) : Entity<DoctorData>(d)
{
    public Doctor() : this(null) { }
    public string? FirstName => data?.FirstName;
    public string? LastName => data?.LastName;
    public Specialities? Specialization => data?.Specialization;
    public long? PhoneNumber => data?.PhoneNumber;
    public string? EmailAddress => data?.EmailAddress;
    public string FullName => $"{FirstName} {LastName}";

    public List<Appointment> appointments = [];
    public List<Patient?> Patients => appointments?
        .Where(r => r.Patient is not null)
        .Select(r => r.Patient)
        .ToList() ?? [];

    public override async Task LoadLazy()
    {
        await base.LoadLazy();
        appointments.Clear();

        var repo = Services.Get<IAppointmentsRepo>();
        if (repo is null) throw new InvalidOperationException("IAppointmentsRepo service not found.");

        var roles = await repo.GetAsync(nameof(Appointment.DoctorId), Id ?? 0);
        foreach (var r in roles)
        {
            await r.LoadLazy();
            appointments.Add(r);
        }
    }
}