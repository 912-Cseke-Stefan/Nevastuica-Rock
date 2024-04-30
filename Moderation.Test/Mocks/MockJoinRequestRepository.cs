using Moderation.DbEndpoints;
using Moderation.Entities;
using Moderation.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moderation.Test.Mocks
{
    internal class MockJoinRequestRepository : JoinRequestRepository
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
