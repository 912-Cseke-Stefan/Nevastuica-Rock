using Moderation.Entities;
using Backend.Repository.Interfaces;
using Moderation.Model;

namespace Moderation.Test.Mocks
{
    internal class MockTextPostRepository : ITextPostRepository
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
                new TextPost(Guid.Parse("2077F417-CB31-4728-B5BB-3AA57239BBCD"), "content1", Guid.NewGuid(), new GroupUser(Guid.Parse("B7CCB450-EE32-4BFF-8383-E0A0F36CAC06"), Guid.Parse("3E0F1ED0-8EAF-4D71-AFC7-07D62FFEF973"))),
                new TextPost("content2", new GroupUser(Guid.Parse("0825D1FD-C40B-4926-A128-2D924D564B3E"), Guid.Parse("3E0F1ED0-8EAF-4D71-AFC7-07D62FFEF973"))),
                new TextPost("content3", new GroupUser(Guid.Parse("B7CCB450-EE32-4BFF-8383-E0A0F36CAC06"), Guid.Parse("3E0F1ED0-8EAF-4D71-AFC7-07D62FFEF973")))
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
