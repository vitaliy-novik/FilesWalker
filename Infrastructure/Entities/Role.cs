namespace Infrastructure.Entities
{
    /// <summary>
    /// Class for user roles transfering between DAL and WebSite
    /// </summary>
    public class Role : IRole
    {
        public string Name { get; set; }

        public string Id { get; set; }
    }
}
