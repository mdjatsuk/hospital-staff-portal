using MVC.Data;

namespace MVC.Domain;

public class Doctor(DoctorData d) : Entity<DoctorData>(d)
{
    public string? FirstName => data?.FirstName;
    public string? LastName => data?.LastName;
    public string? Specialization => data?.Specialization;
    public string? PhoneNumber => data?.PhoneNumber;
}