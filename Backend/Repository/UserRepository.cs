using Moderation.Entities;
using Moderation.DbEndpoints;
using Backend.Repository.Interfaces;

namespace Moderation.Repository
{
    public class UserRepository : IUserRepository
    {
        protected readonly Dictionary<Guid, User> data;

        public UserRepository(Dictionary<Guid, User> data)
        {
            this.data = data;
        }

        public UserRepository() : base()
        {
        }

        public bool Add(Guid key, User value)
        {
            UserEndpoints.CreateUser(value);
            return true;
        }

        public bool Contains(Guid key)
        {
            return UserEndpoints.ReadAllUsers().Exists(u => u.Id == key);
        }

        public User? Get(Guid key)
        {
            return UserEndpoints.ReadAllUsers().Find(u => u.Id == key);
        }

        public IEnumerable<User> GetAll()
        {
            return UserEndpoints.ReadAllUsers();
        }

        public bool Remove(Guid key)
        {
            UserEndpoints.DeleteUser(key);
            return true;
        }

        public bool Update(Guid key, User value)
        {
            UserEndpoints.UpdateUser(value);
            return true;
        }
        public Guid? GetGuidByName(string name)
        {
            return UserEndpoints.ReadAllUsers().Find(u => u.Username == name)?.Id;
        }
    }
}