using MVC.Domain;

namespace MVC.Tests.Domain;

internal class mockMedicalRecordRepo : mockRepo<MedicalRecord>, IMedicalRecordsRepo
{
    public override async Task AddAsync(MedicalRecord o)
    {
        list.Add(o);
        await Task.CompletedTask;
    }

    public override Task<IEnumerable<MedicalRecord>> GetAsync(string propertyName, int idValue)
    {
        var prop = typeof(MedicalRecord).GetProperty(propertyName);
        var result = list.Where(x => {
            var value = prop?.GetValue(x);
            return value is int intVal && intVal == idValue;
        });
        return Task.FromResult(result.AsEnumerable());
    }
}
