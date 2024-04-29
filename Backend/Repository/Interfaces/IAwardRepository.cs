using Moderation.Entities;

namespace Moderation.Repository.Interfaces
{
    public interface IAwardRepository: IRepository<Award>
    {
        bool Add(Guid key, Award value);
        bool Remove(Guid key);
        Award? Get(Guid key);
        IEnumerable<Award> GetAll();
        bool Contains(Guid key);
        bool Update(Guid key, Award value);
    }
}