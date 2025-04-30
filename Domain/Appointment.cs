using MVC.Core;
using MVC.Data;

namespace MVC.Domain;

public sealed class Appointment(AppointmentData? d) : Entity<AppointmentData>(d)
{
    public Appointment() : this(null) {}
    public int DoctorId => data?.DoctorId ?? 0;
    public int PatientId => data?.PatientId ?? 0;
    public DateTime? Date => data?.Date;
    public string? Location => data?.Room;
    public double? AppointmentFee => data?.AppointmentFee;
    public Doctor? Doctor => doctor;
    public Patient? Patient => patient;

    internal Doctor? doctor;

    internal Patient? patient;

    public override async Task LoadLazy()
    {
        await base.LoadLazy();
        var r = Services.Get<IDoctorsRepo>();
        var p = Services.Get<IPatientsRepo>();
        if (p is null) return;
        if (r is null) return;
        doctor = await r.GetAsync(DoctorId)!;
        patient = await p.GetAsync(PatientId)!;
    }
}