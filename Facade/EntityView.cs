using System.ComponentModel.DataAnnotations;

namespace MVC.Facade;

public abstract class EntityView
{
    protected internal const string dateName = "Date";
    protected internal const string reqPrescriptionName = "Requires Prescription";
    protected internal const string firstName = "First Name";
    protected internal const string lastName = "Last Name";
    protected internal const string specializationName = "Specialization";
    protected internal const string phoneNumberName = "Phone Number";
    protected internal const string birthName = "Date Of Birth";
    protected internal const string appointName = "Appointment Fee";
    protected internal const string diagnosis = "Diagnosis";
    protected internal const string diagnoses = "Diagnoses";
    protected internal const string recordnr = "Record Number";
    protected internal const string medicineName = "Medicines";
    protected internal const string doctor = "Doctor";
    protected internal const string patient = "Patient";
    protected internal const string recordNr = "Record Nr";
    protected internal const string doctors = "Doctors";
    protected internal const string patients = "Patients";
    protected internal const string appointments = "Appointments";
    protected internal const string medicalRecords = "Medical Records";
    protected internal const string diagnosedOn = "Diagnosed on";
    protected internal const string emailName = "Email address";

    public const string roomEx = @"^[A-Z]{2}\d{3}$";
    protected const string namesEx = @"^[A-Z][a-zA-Z]*$";
    protected const string capitalLetterEx = @"^[A-Z].*$";
    protected const string phoneNumberEx = @"^\d{8}$";
    protected const string recordNrEx = @"^#(9999|[1-9][0-9]{2,3})$";


    protected internal const string roomError = "A room must start with two capital letters followed by three digits (e.g., AB302).";
    protected internal const string appointError = "Appointment Fee must be zero or positive.";
    protected internal const string descriptionLengthError = "The Description cannot exceed 500 characters.";
    protected internal const string descriptionError = "Description must start with a capital letter.";
    protected internal const string medicineLengthError = "The Diagnosis cannot exceed 500 characters.";
    protected internal const string phoneNumberError = "The phone number must be exactly 8 digits and be between 50000000 and 59999999.";
    protected internal const string firstNameError = "First Name must be between 2 and 50 characters.";
    protected internal const string firstNameLetterError = "First Name must start with a capital letter and contain only letters.";
    protected internal const string lastNameError = "Last Name must be between 2 and 50 characters.";
    protected internal const string lastNameLetterError = "Last Name must start with a capital letter and contain only letters.";
    protected internal const string diagnosisError = "Diagnosis must start with a capital letter.";
    protected internal const string recordNrError = "Record Number must start with # and be a number from 100 to 9999.";
    protected internal const string medicineError = "Medicine must start with a capital letter.";
    protected internal const string requiredError = "This field is required.";



    protected internal const int descriptionLength = 500;
    protected internal const int medicineLength = 500;
    protected internal const int nameLength = 50;
    protected internal const int minNameLength = 2;
    protected internal const double appointMaxFee = 10000;
    protected internal const int appointMinFee = 0;

    public int Id { get; set; }
}
