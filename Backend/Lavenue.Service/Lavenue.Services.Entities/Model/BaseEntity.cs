using System;

namespace Lavenue.Services.Entities.Model
{
    public abstract class BaseEntity
    {
        public string Id { get; set; }
        public DateTime DateCreated  { get; set; }
    }
}