using Backend.Repository.Interfaces;
using Moderation.DbEndpoints;
using Moderation.Entities;

namespace Moderation.Repository
{
    public class JoinRequestRepository : IJoinRequestRepository
    {
        protected readonly Dictionary<Guid, JoinRequest> data;
        public JoinRequestRepository(Dictionary<Guid, JoinRequest> data)
        {
            this.data = data;
        }
        public JoinRequestRepository() : base()
        {
        }

        public bool Add(Guid key, JoinRequest value)
        {
            JoinRequestEndpoints.CreateJoinRequest(value);
            return true;
        }

        public bool Contains(Guid key)
        {
            return JoinRequestEndpoints.ReadAllJoinRequests().Exists(a => a.Id == key);
        }

        public JoinRequest? Get(Guid key)
        {
            return JoinRequestEndpoints.ReadAllJoinRequests().Find(a => a.Id == key);
        }

        public IEnumerable<JoinRequest> GetAll()
        {
            return JoinRequestEndpoints.ReadAllJoinRequests();
        }

        public bool Remove(Guid key)
        {
            JoinRequestEndpoints.DeleteJoinRequest(key);
            return true;
        }

        public bool Update(Guid key, JoinRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
