using Backend.Repository.Interfaces;
using Moderation.Entities;
using Moderation.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Moderation.Test.Mocks
{
    internal class MockPostRepository : IPostRepository
    {
        public bool Add(Guid key, IPost post)
        {
            return true;
        }

        public bool Remove(Guid key)
        {
            return true;
        }

        public IPost? Get(Guid key)
        {
            return new TextPost("content", new GroupUser(new Guid(), new Guid()));
        }

        public IEnumerable<IPost> GetAll()
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

        public bool Update(Guid key, IPost value)
        {
            return true;
        }
    }
}
