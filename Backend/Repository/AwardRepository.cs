using Backend.Repository.Interfaces;
using Moderation.DbEndpoints;
using Moderation.Entities;

namespace Moderation.Repository
{
    public class AwardRepository : IRepository<Award>
    {
        protected readonly Dictionary<Guid, Award> data;
        public AwardRepository(Dictionary<Guid, Award> data)
        {
            this.data = data;
        }
        public AwardRepository() : base()
        {
        }

        public override bool Add(Guid key, Award value)
        {
            AwardEndpoint.CreateAward(value);
            return true;
        }

        public override bool Contains(Guid key)
        {
            return AwardEndpoint.ReadAwards().Exists(a => a.Id == key);
        }

        public override Award? Get(Guid key)
        {
            return AwardEndpoint.ReadAwards().Find(a => a.Id == key);
        }

        public override IEnumerable<Award> GetAll()
        {
            return AwardEndpoint.ReadAwards();
        }

        public override bool Remove(Guid key)
        {
            AwardEndpoint.DeleteAward(key);
            return true;
        }

        public override bool Update(Guid key, Award award)
        {
            AwardEndpoint.UpdateAward(award);
            return true;
        }
    }
}
