using MVC.Data;

namespace MVC.Domain;

public class Patient(PatientData d) : Entity<PatientData>(d)
{
    public string? FirstName => data?.FirstName;
    public string? LastName => data?.LastName;
    public DateTime? DateOfBirth => data?.DateOfBirth;
    public Genders? Gender => data?.Gender;
}