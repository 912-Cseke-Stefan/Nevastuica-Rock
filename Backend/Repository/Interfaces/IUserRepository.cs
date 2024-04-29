using Moderation.Entities;


namespace Moderation.Repository.Interfaces
{
    internal interface IUserRepository : IRepository<User>
    {
        bool Add(Guid key, User value);
        bool Remove(Guid key);
        User? Get(Guid key);
        IEnumerable<User> GetAll();
        bool Contains(Guid key);
        bool Update(Guid key, User value);
    }
}
