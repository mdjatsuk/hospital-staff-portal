using System.Numerics;

namespace MVC.Facade;

public abstract class EntityView
{
    internal protected const string dateName = "Date";
    internal protected const string reqSurgeryName = "Requires Surgery";
    internal protected const string firstName = "First Name";
    internal protected const string lastName = "Last Name";
    internal protected const string specializationName = "Specialization";
    internal protected const string phoneNumberName = "Phone Number";
    internal protected const string birthName = "Date Of Birth";
    internal protected const string appointName = "Appointment Fee";
    internal protected const string diagnosName = "Diagnosis Name";

    internal protected const string locationEx = @"^[A-Z].*";
    internal protected const string phoneNumberEx = @"^\d{8}$";

    internal protected const string locationError = "Location must start with a capital letter.";
    internal protected const string appointError = "Appointment Fee must be zero or positive.";
    internal protected const string diagnosError = "The Diagnosis Name cannot exceed 100 characters.";
    internal protected const string descriptionError = "The Description cannot exceed 500 characters.";
    internal protected const string phoneNumberError = "The phone number must be exactly 8 digits.";
    internal protected const string firstNameError = "First Name must be between 2 and 50 characters.";
    internal protected const string lastNameError = "Last Name must be between 2 and 50 characters.";

    internal protected const int diagnosLength = 100;
    internal protected const int descriptionLength = 500;
    internal protected const int nameLength = 50;
    internal protected const int minNameLength = 2;
    internal protected const int male = 0;
    internal protected const int female = 1;
    internal protected const double appointMaxFee = double.MaxValue;

    public int Id { get; set; }
}
