using Backend.Repository.Interfaces;
using Moderation.Entities;
using Moderation.Model;

namespace Moderation.Test.Mocks
{
    internal class MockGroupUserRepository : IGroupUserRepository
    {
        protected readonly Dictionary<Guid, GroupUser> Data;
        public MockGroupUserRepository()
        {
            Data = new Dictionary<Guid, GroupUser>();
            for (var i = 0; i < 20; i++)
            {
                GroupUser groupUser = new GroupUser(Guid.NewGuid(), Guid.NewGuid());
                Data.Add(groupUser.Id, groupUser);
            }
        }

        public bool Add(Guid key, GroupUser value)
        {
            Data.Add(key, value);
            return true;
        }

        public bool Remove(Guid key)
        {
            if (Data.ContainsKey(key))
            {
                Data.Remove(key);
                return true;
            }
            return false;
        }

        public GroupUser? Get(Guid key)
        {
            return new GroupUser(
                    key,
                /*User*/Guid.Parse("B7CCB450-EE32-4BFF-8383-E0A0F36CAC06"),   // victor
                /*Group*/Guid.Parse("3E0F1ED0-8EAF-4D71-AFC7-07D62FFEF973"), // victor's study group
                /*Post score*/          1,
                /*Marketplace Score*/   1,
                new UserStatus(UserRestriction.None, DateTime.Now),
                /*Role*/Guid.Parse("00E25F4D-6C60-456B-92CF-D37751176177"));
        }

        public IEnumerable<GroupUser> GetAll()
        {
            return new List<GroupUser>() {
            new GroupUser(
                    Guid.Parse("B05ABC1A-8952-41FB-A503-BFAD23CA9092"),
                /*User*/Guid.Parse("B7CCB450-EE32-4BFF-8383-E0A0F36CAC06"),   // victor
                /*Group*/Guid.Parse("3E0F1ED0-8EAF-4D71-AFC7-07D62FFEF973"), // victor's study group
                /*Post score*/          1,
                /*Marketplace Score*/   1,
                new UserStatus(UserRestriction.None, DateTime.Now),
                /*Role*/Guid.Parse("00E25F4D-6C60-456B-92CF-D37751176177")),
            new GroupUser(
                    Guid.Parse("4CCA015B-D068-43B1-8839-08D767391769"),
                /*User*/Guid.Parse("0825D1FD-C40B-4926-A128-2D924D564B3E"),  // boti
                /*Group*/Guid.Parse("3E0F1ED0-8EAF-4D71-AFC7-07D62FFEF973"), // victor's study group
                /*Post score*/          1,
                /*Marketplace Score*/   1,
                new UserStatus(UserRestriction.None, DateTime.Now),
                /*Role*/Guid.Parse("5B4432BD-7A3C-463C-8A4B-34E4BF452AC3")),
            new GroupUser(
                    Guid.Parse("4017CB13-22B0-43B7-A111-50154C62CC6C"),
                /*User*/Guid.Parse("E17FF7A1-95DF-4EAE-8A69-9B139CCD7CA8"),  // norby
                /*Group*/Guid.Parse("3E0F1ED0-8EAF-4D71-AFC7-07D62FFEF973"), // victor's study group
                /*Post score*/          1,
                /*Marketplace Score*/   1,
                new UserStatus(UserRestriction.None, DateTime.Now),
                /*Role*/Guid.Parse("5DEEE3BF-C6A2-4FD2-8E8E-BCA475F4BD44"))
            };
        }

        public bool Contains(Guid key)
        {
            return Data.ContainsKey(key);
        }

        public bool Update(Guid key, GroupUser value)
        {
            if (Data.ContainsKey(key))
            {
                Data[key] = value;
                return true;
            }
            return false;
        }
    }
}
