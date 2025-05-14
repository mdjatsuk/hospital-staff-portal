using MVC.Data;
using MVC.Domain;

namespace MVC.Facade;

public sealed class AppointmentViewFactory : AbstractViewFactory<AppointmentData, AppointmentView> 
{
    public override async Task<AppointmentView> CreateView(AppointmentData? d, bool loadLazy = false)
    {
        var v = await base.CreateView(d, loadLazy);
        if (!loadLazy) return v;
        var o = new Appointment(d);
        await o.LoadLazy();
        v.DoctorFullName = o.Doctor?.FullName;
        v.PatientFullName = o.Patient?.FullName;
        return v;
    }
}