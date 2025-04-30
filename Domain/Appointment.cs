using MVC.Core;
using MVC.Data;

namespace MVC.Domain;

public class Appointment(AppointmentData d) : Entity<AppointmentData>(d)
{
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
        doctor = await Services.Get<IDoctorsRepo>()?.GetAsync(DoctorId)!;
        patient = await Services.Get<IPatientsRepo>()?.GetAsync(PatientId)!;
    }
}