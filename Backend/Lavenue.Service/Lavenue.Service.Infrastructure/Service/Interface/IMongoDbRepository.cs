using Lavenue.Service.Entities.Model;
using MongoDB.Driver;

namespace Lavenue.Service.Infrastructure.Service.Interface;

public interface IMongoDbRepository<T> where T : BaseEntity
{
    IMongoCollection<T> GetCollection();
    Task InsertUser(T entity);
    Task<List<T>> GetAll();

    Task<T> GetById(string id);
    // Task<bool> UpdateDocument(string id, T entity);
}