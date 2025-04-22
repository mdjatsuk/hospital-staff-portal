using System.ComponentModel.DataAnnotations;

namespace MVC.Domain;

public class Diagnosis : Entity
{
    [Required, Display(Name = "Diagnosis Name")]
    [StringLength(100, ErrorMessage = "The Diagnosis Name cannot exceed 100 characters.")]
    public string? DiagnosisName { get; set; }
    [Required, StringLength(500, ErrorMessage = "The Description cannot exceed 500 characters.")]
    public string? Description { get; set; }
    [Display(Name = "Requires Surgery")]
    public bool RequiresSurgery { get; set; }
}