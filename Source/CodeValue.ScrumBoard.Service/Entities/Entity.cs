using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CodeValue.ScrumBoard.Service.Entities
{
    public abstract class Entity
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public DateTime CreationTimeUtc { get; set; }

        public int Version { get; set; }
    }
}