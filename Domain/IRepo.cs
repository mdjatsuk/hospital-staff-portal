using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Domain
{
    public interface IAppointmentRepo : IRepo<Appointment>;
    public interface IDiagnosisRepo : IRepo<Diagnosis>;
    public interface IDoctorRepo : IRepo<Doctor>;
    public interface IPatientRepo : IRepo<Patient>;
    public interface IRepo<TObject>
    {
        public Task<IEnumerable<TObject>> GetAll();
        public Task<TObject?> GetById(int? id);
        public Task AddAsync(TObject o);
        public Task UpdateAsync(TObject o);
        public Task DeleteAsync(int id);
    }
}
