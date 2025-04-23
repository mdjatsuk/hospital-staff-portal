using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Domain
{
    public interface IAppointmentDataRepo : IRepo<Appointment>;
    public interface IDiagnosisRepo : IRepo<Diagnosis>;
    public interface IDoctorRepo : IRepo<Doctor>;
    public interface IPatientRepo : IRepo<Patient>;
    public interface IRepo<TObject>
    {
        public Task<int> PageCount(byte pageSize, string? filter);
        public Task<IEnumerable<TObject>> GetAsync(int pageIdx, byte pageSize
            , string? orderBy = null, string? filter = null);
        public Task<IEnumerable<TObject>> GetAsync(string propertyName, int idValue);
        public Task<IEnumerable<TObject>> GetAsync();
        public Task<TObject?> GetAsync(int? id);
        public Task AddAsync(TObject o);
        public Task UpdateAsync(TObject o);
        public Task DeleteAsync(int id);
    }
}
