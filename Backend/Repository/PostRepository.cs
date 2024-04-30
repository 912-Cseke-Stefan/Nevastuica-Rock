using Backend.Repository.Interfaces;
using Moderation.Model;

namespace Backend.Repository
{
    public class PostRepository : IPostRepository
    {
        protected readonly Dictionary<Guid, IPost> Data;
        public PostRepository(Dictionary<Guid, IPost> data)
        {
            this.Data = data;
        }

        public PostRepository() : base()
        {
        }

        public virtual bool Add(Guid key, IPost post)
        {
            if (Data.ContainsKey(key))
            {
                return false;
            }

            Data.Add(key, post);
            return true;
        }

        public virtual bool Remove(Guid key)
        {
            return Data.Remove(key);
        }

        public virtual IPost? Get(Guid key)
        {
            var value = Data.GetValueOrDefault(key);
            return value;
        }

        public virtual IEnumerable<IPost> GetAll()
        {
            return Data.Values;
        }

        public virtual bool Contains(Guid key)
        {
            return Data.ContainsKey(key);
        }

        public virtual bool Update(Guid key, IPost value)
        {
            if (!Data.ContainsKey(key))
            {
                return false;
            }

            Data[key] = value;
            return true;
        }
    }
}
