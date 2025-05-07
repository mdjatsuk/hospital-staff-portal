using MVC.Data;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVC.Facade;

[DisplayName(diagnosis)] 
public sealed class DiagnosisView : EntityView
{
    [Required(ErrorMessage = "The Medicine Name field is required.")]
    [StringLength(medicineLength, ErrorMessage = medicineError)]
    [Display(Name = medicineName)]
    public string? MedicineName { get; set; }

    [Required, StringLength(descriptionLength, ErrorMessage = descriptionError)]
    public string? Description { get; set; }
    [Display(Name = reqSurgeryName)]
    public bool RequiresSurgery { get; set; }

    [Display(Name = reqPrescriptionName)]
    public bool RequiresPrescription { get; set; }
}
