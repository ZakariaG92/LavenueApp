using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

namespace Lavenue.Service.Entities.Model;

public class User : BaseEntity
{
    [BsonElement] public string FirstName { get; set; }
    [BsonElement] public string LastName { get; set; }
    [BsonElement] public string Address { get; set; }
    [BsonElement] [EmailAddress] public string Email { get; set; }
    [BsonElement] public string? PhoneNumber { get; set; }
}