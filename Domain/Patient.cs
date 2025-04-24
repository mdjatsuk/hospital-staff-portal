using MVC.Core;
using MVC.Data;
using System.Numerics;

namespace MVC.Domain;

public class Patient(PatientData d) : Entity<PatientData>(d)
{
    public string? FirstName => data?.FirstName;
    public string? LastName => data?.LastName;
    public DateTime? DateOfBirth => data?.DateOfBirth;
    public Genders? Gender => data?.Gender;
    public string FullName => $"{FirstName} {LastName}";
}