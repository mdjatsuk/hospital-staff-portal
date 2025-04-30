using System.Numerics;

namespace MVC.Facade;

public abstract class EntityView
{
    protected internal const string dateName = "Date";
    protected internal const string reqSurgeryName = "Requires Surgery";
    protected internal const string firstName = "First Name";
    protected internal const string lastName = "Last Name";
    protected internal const string specializationName = "Specialization";
    protected internal const string phoneNumberName = "Phone Number";
    protected internal const string birthName = "Date Of Birth";
    protected internal const string appointName = "Appointment Fee";
    protected internal const string diagnosName = "Diagnosis Name";
    protected internal const string diagnosis = "Diagnosis";
    protected internal const string doctor = "Doctor";
    protected internal const string patient = "Patient";
    protected internal const string doctors = "Doctors";
    protected internal const string patients = "Patients";
    protected internal const string appointments = "Appointments";

    public const string locationEx = @"^[A-Z].*";
    protected internal const string phoneNumberEx = @"^\d{8}$";

    protected internal const string locationError = "Location must start with a capital letter.";
    protected internal const string appointError = "Appointment Fee must be zero or positive.";
    protected internal const string descriptionError = "The Description cannot exceed 500 characters.";
    protected internal const string phoneNumberError = "The phone number must be exactly 8 digits.";
    protected internal const string firstNameError = "First Name must be between 2 and 50 characters.";
    protected internal const string lastNameError = "Last Name must be between 2 and 50 characters.";

    protected internal const int descriptionLength = 500;
    protected internal const int nameLength = 50;
    protected internal const int minNameLength = 2;
    protected internal const int male = 0;
    protected internal const int female = 1;
    protected internal const double appointMaxFee = double.MaxValue;
    protected internal const int appointMinFee = 0;

    public int Id { get; set; }
}
