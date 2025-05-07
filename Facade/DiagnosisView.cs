using MVC.Data;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVC.Facade;

[DisplayName(diagnosis)] 
public sealed class DiagnosisView : EntityView
{
    [Display(Name = "Medicine"), Required]
    public string? MedicineName { get; set; }

    [Required, StringLength(descriptionLength, ErrorMessage = descriptionError)]
    public string? Description { get; set; }
    [Display(Name = reqSurgeryName)]
    public bool RequiresSurgery { get; set; }

    [Display(Name = "Requires Prescription")]
    public bool RequiresPrescription { get; set; }
}
