using Moderation.Entities;

namespace Backend.Repository.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        public Guid? GetGuidByName(string name);
    }
}
