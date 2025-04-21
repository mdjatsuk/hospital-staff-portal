using MVC_Project.Domain;
using MVC_Project.Soft.Data;

namespace MVC_Project.Soft.Controllers
{
    public class DiagnosisController(ApplicationDbContext c) : BaseController<Diagnosis>(c) { }
}