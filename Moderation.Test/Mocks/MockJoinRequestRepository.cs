using Moderation.Entities;
using Backend.Repository.Interfaces;

namespace Moderation.Test.Mocks
{
    internal class MockJoinRequestRepository : IJoinRequestRepository
    {
        public bool Add(Guid key, JoinRequest value)
        {
            return true;
        }

        public bool Contains(Guid key)
        {
            return true;
        }

        public JoinRequest? Get(Guid key)
        {
            return new JoinRequest(Guid.NewGuid());
        }

        public IEnumerable<JoinRequest> GetAll()
        {
            return new List<JoinRequest>() { 
                new JoinRequest(Guid.NewGuid()),
                new JoinRequest(Guid.NewGuid()),
                new JoinRequest(Guid.NewGuid())
            };
        }

        public bool Remove(Guid key)
        {
            return true;
        }
    }
}
