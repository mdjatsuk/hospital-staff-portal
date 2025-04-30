using MVC.Data;
using MVC.Domain;
using MVC.Facade;
using MVC.Soft.Data;

namespace MVC.Soft.Controllers
{
    public class MedicalRecordsController(ApplicationDbContext c)
        : BaseController<MedicalRecord, MedicalRecordData, MedicalRecordView>(c, new MedicalRecordViewFactory(), d => new(d))
    { }
}
