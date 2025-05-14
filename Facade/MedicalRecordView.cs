using MVC.Aids.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVC.Facade;

[DisplayName(medicalRecords)] public sealed class MedicalRecordView : EntityView
{
    [Display(Name = patient)] public int PatientId { get; set; }
    [Display(Name = recordNr)] public int RecordNrId { get; set; }
    [Display(Name = patient)] public string? Patient { get; set; }
    [Display(Name = recordNr)] public string? RecordNr { get; set; }
    [Display(Name = patient)] public string? PatientFullName { get; set; }
    [Display(Name = diagnosedOn), Required(ErrorMessage = requiredError), DataType(DataType.Date), DateTodayOnlyValidation] 
    public DateTime? DiagnosedOn { get; set; }

}