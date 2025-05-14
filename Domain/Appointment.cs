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

    public string? DoctorFullName => Doctor?.FullName;
    public string? PatientFullName => Patient?.FullName;
    public override async Task LoadLazy()
    {
        await base.LoadLazy();
        doctor = await getItem<IDoctorsRepo,Doctor>(DoctorId);
        patient = await getItem<IPatientsRepo,Patient>(PatientId)!;
    }
}