using Backend.Repository.Interfaces;
using Moderation.Entities;

namespace Moderation.Test.Mocks
{
    internal class MockUserRepository : IUserRepository
    {
        public bool Add(Guid key, User value)
        {
            return true;
        }

        public bool Remove(Guid key)
        {
            return true;
        }

        public User? Get(Guid key)
        {
            // Creating a new user with a default constructor
            return new User(key, "TestUser", "TestPassword");
        }

        public IEnumerable<User> GetAll()
        {
            // Creating users with different constructors
            return new List<User>()
            {
                new User(Guid.Parse("B7CCB450-EE32-4BFF-8383-E0A0F36CAC06"), "TestUser1"),
                new User("TestUser2"),
                new User("TestUser3")
            };
        }

        public bool Contains(Guid key)
        {
            return true;
        }

        public bool Update(Guid key, User value)
        {
            return true;
        }

        public Guid? GetGuidByName(string name)
        {
            return Guid.Parse("B05ABC1A-8952-41FB-A503-BFAD23CA9092");
        }
    }
}
