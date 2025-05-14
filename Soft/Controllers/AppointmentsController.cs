using Microsoft.AspNetCore.Mvc;
using MVC.Data;
using MVC.Domain;
using MVC.Facade;
using MVC.Soft.Data;

namespace MVC.Soft.Controllers
{
    public class AppointmentsController : BaseController<Appointment, AppointmentData, AppointmentView>
    {
        private readonly ApplicationDbContext _context;

        public AppointmentsController(ApplicationDbContext c)
            : base(c, new AppointmentViewFactory(), d => new Appointment(d))
        {
            _context = c;
        }

        [HttpPost, ValidateAntiForgeryToken]
        public override async Task<IActionResult> Create(AppointmentView v)
        {
            if (!ModelState.IsValid) return View(v);

            var d = f.CreateData(v);

            await SetFullNames(d);

            await r.AddAsync(createObject(d)); 
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public override async Task<IActionResult> Edit(int id, AppointmentView v)
        {
            if (id != v.Id) return NotFound();
            if (!ModelState.IsValid) return View(v);

            var d = f.CreateData(v);
            await SetFullNames(d);

            await r.UpdateAsync(createObject(d)); 
            return RedirectToAction(nameof(Index));
        }

        private async Task SetFullNames(AppointmentData d)
        {
            var doctor = await _context.Doctors.FindAsync(d.DoctorId);
            var patient = await _context.Patients.FindAsync(d.PatientId);

            if (doctor != null)
                d.DoctorFullName = $"{doctor.FirstName} {doctor.LastName}";

            if (patient != null)
                d.PatientFullName = $"{patient.FirstName} {patient.LastName}";
        }
    }
}


