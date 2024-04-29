using Moderation.Entities;

namespace Backend.Repository.Interfaces
{
    public interface IRepository<T>
        where T : IHasID
    {
        bool Add(Guid key, T value);
        bool Remove(Guid key);
        T? Get(Guid key);
        IEnumerable<T> GetAll();
        bool Contains(Guid key);
        bool Update(Guid key, T value);
    }
}