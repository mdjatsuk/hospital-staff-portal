using MVC.Data;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVC.Facade;

[DisplayName(diagnoses)] 
public sealed class DiagnosisView : EntityView
{
    [Required, Display(Name = recordnr)]
    public string? RecordNr { get; set; }

    [Required, Display(Name = diagnosis)]
    public string? Diagnosis { get; set; }

    [Required, StringLength(descriptionLength, ErrorMessage = descriptionError)]
    public string? Description { get; set; }

    [Required(ErrorMessage = "The Diagnosis Name field is required.")]
    [StringLength(medicineLength, ErrorMessage = medicineError)]
    [Display(Name = medicineName)]
    public string? Medicine { get; set; }

    [Display(Name = reqPrescriptionName)]
    public bool RequiresPrescription { get; set; }
}
