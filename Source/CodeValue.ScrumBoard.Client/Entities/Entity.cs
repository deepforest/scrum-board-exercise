using System;

namespace CodeValue.ScrumBoard.Service.Entities
{
    public abstract class Entity
    {
        public string Id { get; set; }

        public DateTime CreationTimeUtc { get; set; }

        public int Version { get; set; }
    }
}