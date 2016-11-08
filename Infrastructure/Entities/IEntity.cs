using System;

namespace Infrastructure.Entities
{
    /// <summary>
    /// Interface for entities with Id property
    /// </summary>
    public interface IEntity
    {
        string Id { get; set; }
    }
}
