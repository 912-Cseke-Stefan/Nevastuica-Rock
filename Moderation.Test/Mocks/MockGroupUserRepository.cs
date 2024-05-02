using Backend.Repository.Interfaces;
using Moderation.Entities;

namespace Moderation.Test.Mocks
{
    internal class MockGroupUserRepository : IGroupUserRepository
    {
        protected readonly Dictionary<Guid, GroupUser> Data;
        public MockGroupUserRepository()
        {
            Data = new Dictionary<Guid, GroupUser>();
            for (var i = 0; i < 20; i++)
            {
                GroupUser groupUser = new GroupUser(Guid.NewGuid(), Guid.NewGuid());
                Data.Add(groupUser.Id, groupUser);
            }
        }

        public bool Add(Guid key, GroupUser value)
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

        public GroupUser? Get(Guid key)
        {
            if (Data.ContainsKey(key))
            {
                return Data[key];
            }
            return null;
        }

        public IEnumerable<GroupUser> GetAll()
        {
            return Data.Values;
        }

        public bool Contains(Guid key)
        {
            return Data.ContainsKey(key);
        }

        public bool Update(Guid key, GroupUser value)
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
