using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVC.Facade;

[DisplayName(diagnoses)] 
public sealed class DiagnosisView : EntityView
{
    [Required(ErrorMessage = requiredError), Display(Name = recordnr), RegularExpression(recordNrEx, ErrorMessage = recordNrError)]
    public string? RecordNr { get; set; }

    [Required(ErrorMessage = requiredError), Display(Name = diagnosis), RegularExpression(capitalLetterEx, ErrorMessage = diagnosisError)]
    public string? Diagnosis { get; set; }

    [Required(ErrorMessage = requiredError), StringLength(descriptionLength, ErrorMessage = descriptionLengthError), 
        RegularExpression(capitalLetterEx, ErrorMessage = descriptionError)]
    public string? Description { get; set; }

    [Required(ErrorMessage = requiredError), Display(Name = medicineName),
        StringLength(medicineLength, ErrorMessage = medicineLengthError), 
        RegularExpression(capitalLetterEx, ErrorMessage = medicineError)]
    public string? Medicine { get; set; }

    [Display(Name = reqPrescriptionName)] public bool RequiresPrescription { get; set; }
}

