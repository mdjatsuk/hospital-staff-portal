using System.ComponentModel.DataAnnotations;

namespace MVC_Project.Domain
{
    public class Patient : Entity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        [RegularExpression("Male|Female")] string? Gender { get; set; }
    }
}
