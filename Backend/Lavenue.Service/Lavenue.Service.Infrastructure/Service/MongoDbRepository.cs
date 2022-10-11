using Lavenue.Service.Common.Model;
using Lavenue.Service.Entities.Model;
using Lavenue.Service.Infrastructure.Service.Interface;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Lavenue.Service.Infrastructure.Service;

public class MongoDbRepository<T> : IMongoDbRepository<T> where T : BaseEntity
{
    private readonly IMongoClient _client;
    private readonly IConfiguration _configuration;
    private readonly IMongoCollection<T> _collection;
    private readonly IMongoDatabase _database;
    private readonly IMongoDbSettings _dbSettings;


    public MongoDbRepository(IMongoDbSettings dbSettings, IMongoClient client, IConfiguration configuration)
    {
        _configuration = configuration;
        _dbSettings = dbSettings;
        _client = client;
        _database = _client.GetDatabase(_dbSettings.DatabaseName);
        _collection = _database.GetCollection<T>(_configuration[$"MongoCollections:{typeof(T).Name}"]);
    }

    public IMongoCollection<T> GetCollection()
    {
        return _collection;
    }

    public async Task InsertUser(T entity)
    {
        await _collection.InsertOneAsync(entity);
    }

    public async Task<List<T>> GetAll()
    {
        return await _collection.Find(e => e.IsDeleted == false).ToListAsync();
    }


    public async Task<T> GetById(string id)
    {
        return await _collection.Find(Builders<T>.Filter.Eq("_id", id)).FirstOrDefaultAsync();
    }

    // public async Task<bool> UpdateDocument(string id, T entity)
    // {
    //     var result = await _collection.ReplaceOneAsync(s => s.Id == id, entity);
    //     return result.ModifiedCount != 0;
    // }
    public void Dispose()
    {
    }
}