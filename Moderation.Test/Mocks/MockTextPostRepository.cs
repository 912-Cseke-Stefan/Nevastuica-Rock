using Moderation.Entities;
using Moderation.Repository;
using Moderation.Model;

namespace Moderation.Test.Mocks
{
    internal class MockTextPostRepository : TextPostRepository
    {
        public bool Add(Guid key, TextPost value)
        {
            return true;
        }

        public bool Remove(Guid key)
        {
            return true;
        }

        public TextPost? Get(Guid key)
        {
            return new TextPost("content", new GroupUser(new Guid(), new Guid()));
        }

        public IEnumerable<TextPost> GetAll()
        {
            return new List<TextPost>() { 
                new TextPost("content1", new GroupUser(new Guid(), new Guid())),
                new TextPost("content2", new GroupUser(new Guid(), new Guid())),
                new TextPost("content3", new GroupUser(new Guid(), new Guid()))
            };
        }

        public bool Contains(Guid key)
        {
            return true;
        }

        public bool Update(Guid key, TextPost value)
        {
            return true;
        }   
    }
}
