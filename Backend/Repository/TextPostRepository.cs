using Backend.Repository.Interfaces;
using Moderation.DbEndpoints;
using Moderation.Model;

namespace Moderation.Repository
{
    public class TextPostRepository : ITextPostRepository
    {
        protected readonly Dictionary<Guid, TextPost> Data;

        public TextPostRepository(Dictionary<Guid, TextPost> data)
        {
            this.Data = data;
        }
        public TextPostRepository() : base()
        {
        }

        public bool Add(Guid key, TextPost value)
        {
            TextPostEndpoints.CreateTextPost(value);
            return true;
        }

        public bool Contains(Guid key)
        {
            return TextPostEndpoints.ReadAllTextPosts().Exists(a => a.Id == key);
        }

        public TextPost? Get(Guid key)
        {
            return TextPostEndpoints.ReadAllTextPosts().Find(a => a.Id == key);
        }

        public IEnumerable<TextPost> GetAll()
        {
            return TextPostEndpoints.ReadAllTextPosts();
        }

        public bool Remove(Guid key)
        {
            TextPostEndpoints.DeleteTextPost(key);
            return true;
        }

        public bool Update(Guid key, TextPost value)
        {
            TextPostEndpoints.UpdateTextPost(value);
            return true;
        }
    }
}
