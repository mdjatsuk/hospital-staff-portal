using MVC.Data;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVC.Facade;

[DisplayName(diagnosis)] 
public sealed class DiagnosisView : EntityView
{
    [Required(ErrorMessage = "The Medicine Name field is required.")]
    [StringLength(30, MinimumLength = 1, ErrorMessage = "The Medicine Name must be between 1 and 30 characters long.")]
    [Display(Name = "Medicine")]
    public string? MedicineName { get; set; }

    [Required, StringLength(descriptionLength, ErrorMessage = descriptionError)]
    public string? Description { get; set; }
    [Display(Name = reqSurgeryName)]
    public bool RequiresSurgery { get; set; }

    [Display(Name = "Requires Prescription")]
    public bool RequiresPrescription { get; set; }
}
