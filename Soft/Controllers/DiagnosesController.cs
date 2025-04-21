using MVC_Project.Domain;
using MVC_Project.Soft.Data;

namespace MVC_Project.Soft.Controllers
{
    public class DiagnosesController(ApplicationDbContext c) : BaseController<Diagnosis>(c) { }
}