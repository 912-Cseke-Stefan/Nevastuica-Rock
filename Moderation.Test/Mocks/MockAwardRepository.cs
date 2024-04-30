using Backend.Repository.Interfaces;
using Moderation.Entities;

namespace Moderation.Test.Mocks
{
    internal class MockAwardRepository : IAwardRepository
    {
        public bool Add(Guid key, Award value)
        {
            return true;
        }

        public bool Remove(Guid key)
        { 
            return true; 
        }

        public Award? Get(Guid key)
        {
            return new Award(key, Award.AwardType.Gold);
        }

        public IEnumerable<Award> GetAll()
        {
            return new List<Award>() { new Award(Guid.NewGuid(), Award.AwardType.Gold),
                                       new Award(Guid.NewGuid(), Award.AwardType.Silver),
                                       new Award(Guid.NewGuid(), Award.AwardType.Bronze)
            };
        }

        public bool Contains(Guid key)
        {
            return true;
        }

        public bool Update(Guid key, Award value)
        {
            return true;
        }
    }
}
