using Microsoft.AspNetCore.Mvc;
using MVC.Data;
using MVC.Domain;
using MVC.Facade;
using MVC.Soft.Data;

namespace MVC.Soft.Controllers;

public class MedicalRecordsController : BaseController<MedicalRecord, MedicalRecordData, MedicalRecordView>
{
    private readonly ApplicationDbContext _context;

    public MedicalRecordsController(ApplicationDbContext c)
        : base(c, new MedicalRecordViewFactory(), d => new MedicalRecord(d))
    {
        _context = c;
    }

    [HttpPost, ValidateAntiForgeryToken]
    public override async Task<IActionResult> Create(MedicalRecordView v)
    {
        if (!ModelState.IsValid) return View(v);

        var d = f.CreateData(v);
        await SetRelatedFields(d);

        await r.AddAsync(createObject(d));
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public override async Task<IActionResult> Edit(int id, MedicalRecordView v)
    {
        if (id != v.Id) return NotFound();
        if (!ModelState.IsValid) return View(v);

        var d = f.CreateData(v);
        await SetRelatedFields(d);

        await r.UpdateAsync(createObject(d));
        return RedirectToAction(nameof(Index));
    }


    private async Task SetRelatedFields(MedicalRecordData d)
    {
        var patient = await _context.Patients.FindAsync(d.PatientId);
        d.PatientFullName = patient != null ? $"{patient.FirstName} {patient.LastName}" : null;

        var recordNr = await _context.Diagnoses.FindAsync(d.RecordNrId); 
        d.RecordNr = recordNr?.RecordNr;
    }
}
