using Backend.Repository.Interfaces;
using Moderation.Model;
using Moderation.Entities;

namespace Moderation.Test.Mocks
{
    internal class MockRoleRepository : IRoleRepository
    {

        protected readonly Dictionary<Guid, Role> Data;

        public MockRoleRepository()
        {
            Data = new Dictionary<Guid, Role>();
            for (var i = 0; i < 20; i++)
            {
                Role role = new Role("Role " + i, new List<Permission>());
                Data.Add(role.Id, role);
            }
        }

        public bool Add(Guid key, Role value)
        {
            Data.Add(key, value);
            return true;
        }

        public bool Remove(Guid key)
        {
            if (Data.ContainsKey(key))
            {
                Data.Remove(key);
                return true;
            }
            return false;
        }

        public Role? Get(Guid key)
        {
            if (Data.ContainsKey(key))
            {
                return Data[key];
            }
            return null;
        }

        public IEnumerable<Role> GetAll()
        {
            return Data.Values;
        }

        public bool Contains(Guid key)
        {
            return Data.ContainsKey(key);
        }

        public bool Update(Guid key, Role value)
        {
            if (Data.ContainsKey(key))
            {
                Data[key] = value;
                return true;
            }
            return false;
        }
    }
}
