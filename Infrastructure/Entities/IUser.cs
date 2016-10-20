namespace Infrastructure.Entities
{
    public interface IUser : IEntity
    {
        string UserName { get; set; }

        string Email { get; set; }

        string Password { get; set; }
    }
}
