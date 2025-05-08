using MVC.Data;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVC.Facade;

[DisplayName(medicineName)] 
public sealed class MedicineView : EntityView
{
    [Required(ErrorMessage = "The Medicine Name field is required.")]
    [StringLength(medicineLength, ErrorMessage = medicineError)]
    [Display(Name = medicineName)]
    public string? MedicineName { get; set; }

    [Required, StringLength(descriptionLength, ErrorMessage = descriptionError)]
    public string? Description { get; set; }

    [Display(Name = reqPrescriptionName)]
    public bool RequiresPrescription { get; set; }
}
