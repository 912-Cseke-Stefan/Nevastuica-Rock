using Backend.Repository.Interfaces;
using Moderation.DbEndpoints;
using Moderation.Entities;
using Moderation.Model;
using Moderation.Repository;

namespace Moderation.Test.Mocks
{
    internal class MockReportRepository : ReportRepository
    {
        public bool Add(Guid key, PostReport value)
        {
            return true;
        }

        public bool Contains(Guid key)
        {
            return true;
        }

        public PostReport? Get(Guid key)
        {
            return new PostReport(new Guid(), new Guid(),"message", new Guid());
        }

        public IEnumerable<PostReport> GetAll()
        {
            return new List<PostReport>() { 
                new PostReport(new Guid(), new Guid(),"message1", new Guid()),
                new PostReport(new Guid(), new Guid(),"message2", new Guid()),
                new PostReport(new Guid(), new Guid(),"message3", new Guid())
            };
        }

        public bool Remove(Guid key)
        {
            return true;
        }

        public bool Update(Guid key, PostReport value)
        {
            return true;
        }
    }
}
