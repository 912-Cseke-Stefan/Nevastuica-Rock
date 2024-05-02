using Backend.Repository.Interfaces;
using Moderation.Model;

namespace Moderation.Test.Mocks
{
    internal class MockReportRepository : IReportRepository
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
                new PostReport(new Guid(), new Guid(),"message1", Guid.Parse("3E0F1ED0-8EAF-4D71-AFC7-07D62FFEF973")),
                new PostReport(new Guid(), new Guid(),"message2", Guid.Parse("3E0F1ED0-8EAF-4D71-AFC7-07D62FFEF973")),
                new PostReport(new Guid(), Guid.Parse("2077F417-CB31-4728-B5BB-3AA57239BBCD"),"message3", Guid.Parse("3E0F1ED0-8EAF-9999-9999-07D62FFEF973"))
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
