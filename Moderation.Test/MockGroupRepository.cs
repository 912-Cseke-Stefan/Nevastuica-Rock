using Backend.Repository.Interfaces;
using Moderation.Model;
using Moderation.Entities;

namespace Moderation.Test
{
    internal class MockGroupRepository : IGroupRepository
    {
        protected readonly Dictionary<Guid, Group> Data;
        public MockGroupRepository()
        {
            this.Data = new Dictionary<Guid, Group>();
            for(var i = 0; i < 20; i++)
            {
                Group group = new Group("Group " + i, "Description " + i, new User("User " + i));
                Data.Add(group.Id, group);
            }
        }

        public bool Add(Guid key, Group value)
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

        public Group? Get(Guid key)
        {
            if (Data.ContainsKey(key))
            {
                return Data[key];
            }
            return null;
        }

        public IEnumerable<Group> GetAll()
        {
            return Data.Values;
        }

        public bool Contains(Guid key)
        {
            return Data.ContainsKey(key);
        }

        public bool Update(Guid key, Group value)
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
