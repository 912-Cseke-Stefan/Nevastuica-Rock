using Backend.Repository.Interfaces;
using Moderation.DbEndpoints;
using Moderation.Entities;
using Moderation.Model;

namespace Moderation.Repository
{
    public class ReportRepository : IReportRepository
    {
        protected readonly Dictionary<Guid, PostReport> data;
        public ReportRepository(Dictionary<Guid, PostReport> data)
        {
            this.data = data;
        }
        public ReportRepository() : base()
        {
        }

        public bool Add(Guid key, PostReport value)
        {
            ReportEndpoint.CreatePostReport(value);
            return true;
        }

        public bool Contains(Guid key)
        {
            return ReportEndpoint.ReadAllPostReports().Exists(r => r.Id == key);
        }

        public PostReport? Get(Guid key)
        {
            return ReportEndpoint.ReadAllPostReports().Find(r => r.Id == key);
        }

        public IEnumerable<PostReport> GetAll()
        {
            return ReportEndpoint.ReadAllPostReports();
        }

        public bool Remove(Guid key)
        {
            ReportEndpoint.DeletePostReport(key);
            return true;
        }

        public bool Update(Guid key, PostReport value)
        {
            ReportEndpoint.UpdatePostReport(key, value);
            return true;
        }
    }
}