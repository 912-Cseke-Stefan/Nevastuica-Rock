using Backend.Repository.Interfaces;
using Moderation.Model;
using Moderation.Entities;

namespace Moderation.Test.Mocks
{
    internal class MockGroupRepository : IGroupRepository
    {
        protected readonly Dictionary<Guid, Group> Data;
        public MockGroupRepository()
        {
            Data = new Dictionary<Guid, Group>();
            for (var i = 0; i < 20; i++)
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
            return new List<Group> {
                new Group (
                    Guid.Parse("BC5F8CED-50D2-4EF3-B3FD-18217D3F9F3A"),
                    "Izabella's birthday party",
                    "balabla",
                    new User("Izabella")),
                new Group (
                    Guid.Parse("3E0F1ED0-8EAF-4D71-AFC7-07D62FFEF973"),
                    "Victor's study group",
                    "none provided",
                    new User("Victor"))
            };
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
