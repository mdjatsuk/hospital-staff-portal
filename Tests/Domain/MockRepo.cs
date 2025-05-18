using MVC.Core;
using MVC.Domain;

namespace MVC.Tests.Domain;

internal class mockRepo<TObject> : IRepo<TObject> where TObject : IEntity
{
    internal List<TObject> list { get; set; } = [];
    public virtual async Task AddAsync(TObject o) => throw new NotImplementedException();
    public virtual Task DeleteAsync(int id) => throw new NotImplementedException();
    public virtual Task<IEnumerable<TObject>> GetAsync(int pageIdx, byte pageSize,
        string? orderBy = null, string? filter = null) => throw new NotImplementedException();
    public virtual Task<IEnumerable<TObject>> GetAsync(string propertyName, int idValue)
        => throw new NotImplementedException();
    public virtual async Task<IEnumerable<TObject>> GetAsync() => throw new NotImplementedException();
    public virtual async Task<TObject?> GetAsync(int? id)
    {
        await Task.CompletedTask;
        return list.FirstOrDefault(x => x.Id == id);
    }
    public virtual Task<int> PageCount(byte pageSize, string? filter) => throw new NotImplementedException();
    public virtual Task UpdateAsync(TObject o) => throw new NotImplementedException();
}

