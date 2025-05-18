using MVC.Domain;

namespace MVC.Tests.Domain;

internal class mockPatientRepo : mockRepo<Patient>, IPatientsRepo
{
    public List<Patient> list { get; set; } = new();

    public override Task<Patient?> GetAsync(int? id)
        => Task.FromResult(list.FirstOrDefault(p => p.Id == id));

    public override Task<IEnumerable<Patient>> GetAsync()
        => Task.FromResult(list.AsEnumerable());

    public override Task<IEnumerable<Patient>> GetAsync(string propertyName, int idValue)
    {
        // Implement as needed, e.g., by property name
        return Task.FromResult(list.AsEnumerable());
    }

    public override Task AddAsync(Patient o)
    {
        list.Add(o);
        return Task.CompletedTask;
    }

    public override Task DeleteAsync(int id)
    {
        var item = list.FirstOrDefault(p => p.Id == id);
        if (item != null) list.Remove(item);
        return Task.CompletedTask;
    }

    public override Task<IEnumerable<Patient>> GetAsync(int pageIdx, byte pageSize, string? orderBy = null, string? filter = null)
        => Task.FromResult(list.Skip(pageIdx * pageSize).Take(pageSize).AsEnumerable());

    public override Task<int> PageCount(byte pageSize, string? filter)
        => Task.FromResult((int)Math.Ceiling((double)list.Count / pageSize));

    public override Task UpdateAsync(Patient o)
    {
        var idx = list.FindIndex(p => p.Id == o.Id);
        if (idx >= 0) list[idx] = o;
        return Task.CompletedTask;
    }
}