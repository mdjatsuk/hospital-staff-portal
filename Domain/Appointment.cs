using MVC.Data;

namespace MVC.Domain;

public class Appointment(AppointmentData d) : Entity<AppointmentData>(d)
{
    public DateTime? Date => data?.Date;
    public string? Location => data?.Location;
    public double? AppointmentFee => data?.AppointmentFee;
}