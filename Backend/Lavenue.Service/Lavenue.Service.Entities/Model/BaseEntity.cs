using MongoDB.Bson.Serialization.Attributes;

namespace Lavenue.Service.Entities.Model;

public abstract class BaseEntity
{
    [BsonId] [BsonElement("_id")] public string Id { get; set; }

    [BsonElement] [BsonDateTimeOptions] public DateTime DateCreated { get; set; }

    [BsonElement] public bool IsDeleted { get; set; }
}