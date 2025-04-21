using System.ComponentModel.DataAnnotations;

namespace MVC.Domain
{
    public class Patient : Entity
    {
        [Display(Name = "First Name")] public string? FirstName { get; set; }
        [Display(Name = "Last Name")] public string? LastName { get; set; }
        [Display(Name = "Date Of Birth"), DataType(DataType.Date)] public DateTime? DateOfBirth { get; set; }
        [RegularExpression("Male|Female")] public string? Gender { get; set; }
    }
}
