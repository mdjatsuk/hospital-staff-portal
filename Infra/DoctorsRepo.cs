using Microsoft.EntityFrameworkCore;
using MVC.Data;
using MVC.Domain;

namespace MVC.Infra;

public sealed class DoctorsRepo : Repo<Doctor, DoctorData>, IDoctorsRepo
{
    public DoctorsRepo(DbContext db) : base(db, d => new Doctor(d)) { }

    public IEnumerable<Doctor> GetDoctors(string researchString)
    {
        var allDoctors = GetAsync().Result; 
        return allDoctors.Where(d =>
            string.IsNullOrEmpty(researchString) ||
            (d.FirstName?.Contains(researchString) ?? false) ||
            (d.LastName?.Contains(researchString) ?? false)
        );
    }
}
