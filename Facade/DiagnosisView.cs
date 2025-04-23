using System.ComponentModel.DataAnnotations;

namespace MVC.Facade;

public sealed class DiagnosisView : EntityView
{
    [Display(Name = diagnosName), Required, StringLength(diagnosLength, ErrorMessage = diagnosError)]
    public string? DiagnosisName { get; set; }

    [Required, StringLength(descriptionLength, ErrorMessage = descriptionError)]
    public string? Description { get; set; }
    [Display(Name = reqSurgeryName)]
    public bool RequiresSurgery { get; set; }
}
