namespace Infrastructure.Entities
{
    /// <summary>
    /// Interface for user roles
    /// </summary>
    public interface IRole : IEntity
    {
        string Name { get; set; }
    }
}
