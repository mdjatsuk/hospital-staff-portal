using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using Mono.Cecil.Cil;
using MVC.Core;
using MVC.Data;
using MVC.Domain;
using Random = MVC.Aids.Random;

namespace MVC.Tests.Domain;

internal class mockRepo<TObject> : IRepo<TObject> where TObject : IEntity
{
    internal List<TObject> list { get; set; } = [];
    public async Task AddAsync(TObject o) => throw new NotImplementedException();
    public Task DeleteAsync(int id) => throw new NotImplementedException();
    public Task<IEnumerable<TObject>> GetAsync(int pageIdx, byte pageSize,
        string? orderBy = null, string? filter = null) => throw new NotImplementedException();
    public Task<IEnumerable<TObject>> GetAsync(string propertyName, int idValue)
        => throw new NotImplementedException();
    public async Task<IEnumerable<TObject>> GetAsync() => throw new NotImplementedException();
    public async Task<TObject?> GetAsync(int? id)
    {
        await Task.CompletedTask;
        return list.FirstOrDefault(x => x.Id == id);
    }
    public Task<int> PageCount(byte pageSize, string? filter) => throw new NotImplementedException();
    public Task UpdateAsync(TObject o) => throw new NotImplementedException();
}

