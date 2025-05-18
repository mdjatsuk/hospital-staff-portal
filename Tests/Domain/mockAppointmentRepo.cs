using MVC.Domain;

namespace MVC.Tests.Domain;

internal class mockAppointmentRepo : mockRepo<Appointment>, IAppointmentsRepo
{
    public List<Appointment> list { get; set; } = new();

    public override Task<IEnumerable<Appointment>> GetAsync(string propertyName, int idValue)
    {
        if (propertyName == nameof(Appointment.DoctorId))
            return Task.FromResult(list.Where(a => a.DoctorId == idValue).AsEnumerable());
        if (propertyName == nameof(Appointment.PatientId))
            return Task.FromResult(list.Where(a => a.PatientId == idValue).AsEnumerable());
        return Task.FromResult(Enumerable.Empty<Appointment>());
    }

    public override Task<IEnumerable<Appointment>> GetAsync()
        => Task.FromResult(list.AsEnumerable());

    public override Task<Appointment?> GetAsync(int? id)
        => Task.FromResult(list.FirstOrDefault(a => a.Id == id));

    public override Task AddAsync(Appointment o)
    {
        list.Add(o);
        return Task.CompletedTask;
    }

    public override Task DeleteAsync(int id)
    {
        var item = list.FirstOrDefault(a => a.Id == id);
        if (item != null) list.Remove(item);
        return Task.CompletedTask;
    }

    public override Task<IEnumerable<Appointment>> GetAsync(int pageIdx, byte pageSize, string? orderBy = null, string? filter = null)
        => Task.FromResult(list.Skip(pageIdx * pageSize).Take(pageSize).AsEnumerable());

    public override Task<int> PageCount(byte pageSize, string? filter)
        => Task.FromResult((int)Math.Ceiling((double)list.Count / pageSize));

    public override Task UpdateAsync(Appointment o)
    {
        var idx = list.FindIndex(a => a.Id == o.Id);
        if (idx >= 0) list[idx] = o;
        return Task.CompletedTask;
    }
}