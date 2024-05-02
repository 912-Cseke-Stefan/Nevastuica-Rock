using Moderation.Repository;
using Moderation.Model;
using Moderation.Entities;

namespace Moderation.Test
{
    public class GroupUserRepositoryTest
    {
        private GroupUserRepository GroupUserRepository;
        [SetUp]
        public void Setup()
        {
            GroupUserRepository = new GroupUserRepository();
        }

        [Test]
        public void AddToGroupUserRepository_SuccessiveAdds_ReturnsSuccessiveBool()
        {
            GroupUser groupUser1 = new GroupUser(Guid.NewGuid(), Guid.NewGuid());
            GroupUser groupUser2 = new GroupUser(Guid.NewGuid(), Guid.NewGuid());

            bool result1 = GroupUserRepository.Add(groupUser1.Id, groupUser1);
            bool result2 = GroupUserRepository.Add(groupUser2.Id, groupUser2);

            Assert.IsTrue(result1);
            Assert.IsTrue(result2);

        }

        [Test]
        public void AddToGroupRepository_NewGroup_IncreasesRepositoryCountByOne()
        {
            GroupUser groupUser1 = new GroupUser(Guid.NewGuid(), Guid.NewGuid());
            var result = GroupUserRepository.GetAll();
            var resultArray = result.ToArray();
            var length = resultArray.Length;

            GroupUserRepository.Add(groupUser1.Id, groupUser1);

            var resultAfterAdd = GroupUserRepository.GetAll();
            var resultArrayAfterAdd = resultAfterAdd.ToArray();
            var lengthAfterAdd = resultArrayAfterAdd.Length;

            Assert.That(lengthAfterAdd, Is.EqualTo(length + 1));
        }
        [Test]
        public void ContainsInGroupUserRepository_ExistingGroupUser_ReturnsTrue()
        {
            GroupUser groupUser1 = new GroupUser(Guid.NewGuid(), Guid.NewGuid());
            GroupUser groupUser2 = new GroupUser(Guid.NewGuid(), Guid.NewGuid());

            GroupUserRepository.Add(groupUser1.Id, groupUser1);
            GroupUserRepository.Add(groupUser2.Id, groupUser2);

            bool result = GroupUserRepository.Contains(groupUser1.Id);
            Assert.IsTrue(result);
        }

        [Test]
        public void ContainsInGroupUserRepository_NonExistingGroupUser_ReturnsTrue()
        {
            GroupUser groupUser1 = new GroupUser(Guid.NewGuid(), Guid.NewGuid());
            GroupUser groupUser2 = new GroupUser(Guid.NewGuid(), Guid.NewGuid());

            GroupUserRepository.Add(groupUser1.Id, groupUser1);

            bool result = GroupUserRepository.Contains(groupUser2.Id);
            Assert.IsFalse(result);
        }

        [Test]
        public void GetGroupUserById_ExistingGroupUser_ReturnsTrue()
        {
            GroupUser groupUser1 = new GroupUser(Guid.NewGuid(), Guid.NewGuid());
            GroupUser groupUser2 = new GroupUser(Guid.NewGuid(), Guid.NewGuid());

            GroupUserRepository.Add(groupUser1.Id, groupUser1);
            GroupUserRepository.Add(groupUser2.Id, groupUser2);

            GroupUser result = GroupUserRepository.Get(groupUser1.Id);
            Assert.That(result, Is.EqualTo(groupUser1));
        }

        [Test]
        public void GetGroupUserById_NonExistingGroupUser_ReturnsNull()
        {
            GroupUser groupUser1 = new GroupUser(Guid.NewGuid(), Guid.NewGuid());
            GroupUser groupUser2 = new GroupUser(Guid.NewGuid(), Guid.NewGuid());

            GroupUserRepository.Add(groupUser1.Id, groupUser1);

            GroupUser result = GroupUserRepository.Get(groupUser2.Id);
            Assert.IsNull(result);
        }

        [Test]
        public void GetGroupUserByUserIdAndGroupId_ExistingGroupUser_ReturnsTrue()
        {
            GroupUser groupUser1 = new GroupUser(Guid.NewGuid(), Guid.NewGuid());
            GroupUser groupUser2 = new GroupUser(Guid.NewGuid(), Guid.NewGuid());

            GroupUserRepository.Add(groupUser1.Id, groupUser1);
            GroupUserRepository.Add(groupUser2.Id, groupUser2);

            GroupUser result = GroupUserRepository.GetByUserIdAndGroupId(groupUser2.UserId, groupUser2.GroupId);
            Assert.That(result, Is.EqualTo(groupUser2));
        }

        [Test]
        public void GetAllInGroupUserRepository_ReturnsAllGroupUsers()
        {
            GroupUser groupUser1 = new GroupUser(Guid.NewGuid(), Guid.NewGuid());
            GroupUser groupUser2 = new GroupUser(Guid.NewGuid(), Guid.NewGuid());
            GroupUser groupUser3 = new GroupUser(Guid.NewGuid(), Guid.NewGuid());
            GroupUser groupUser4 = new GroupUser(Guid.NewGuid(), Guid.NewGuid());

            GroupUserRepository.Add(groupUser1.Id, groupUser1);
            GroupUserRepository.Add(groupUser2.Id, groupUser2);
            GroupUserRepository.Add(groupUser3.Id, groupUser3);
            GroupUserRepository.Add(groupUser4.Id, groupUser4);

            var result = GroupUserRepository.GetAll();
            var resultArray = result.ToArray();
            Assert.Contains(groupUser1, resultArray);
            Assert.Contains(groupUser2, resultArray);
            Assert.Contains(groupUser3, resultArray);
            Assert.Contains(groupUser4, resultArray);
        }

        [Test]
        public void RemoveInGroupUserRepository_ExistingGroupUser_ReturnsTrue()
        {
            GroupUser groupUser1 = new GroupUser(Guid.NewGuid(), Guid.NewGuid());
            GroupUser groupUser2 = new GroupUser(Guid.NewGuid(), Guid.NewGuid());

            GroupUserRepository.Add(groupUser1.Id, groupUser1);
            GroupUserRepository.Add(groupUser2.Id, groupUser2);

            GroupUserRepository.Remove(groupUser1.Id);
            bool result = GroupUserRepository.Contains(groupUser1.Id);
            Assert.IsFalse(result);
        }

        [Test]
        public void RemoveInGroupUserRepository_NonExistingGroupUser_ReturnsFalse()
        {
            GroupUser groupUser1 = new GroupUser(Guid.NewGuid(), Guid.NewGuid());
            GroupUser groupUser2 = new GroupUser(Guid.NewGuid(), Guid.NewGuid());

            GroupUserRepository.Add(groupUser1.Id, groupUser1);

            bool result = GroupUserRepository.Remove(groupUser2.Id);
            Assert.IsTrue(result);
        }

        [Test]
        public void UpdateInGroupUserRepository_ExistingGroupUser_ReturnsTrue()
        {
            GroupUser groupUser1 = new GroupUser(Guid.NewGuid(), Guid.NewGuid());
            GroupUser groupUser2 = new GroupUser(Guid.NewGuid(), Guid.NewGuid());

            GroupUserRepository.Add(groupUser1.Id, groupUser1);
            GroupUserRepository.Add(groupUser2.Id, groupUser2);

            GroupUser updatedGroupUser = new GroupUser(Guid.NewGuid(), Guid.NewGuid());
            updatedGroupUser.Id = groupUser1.Id;

            GroupUserRepository.Update(groupUser1.Id, updatedGroupUser);
            var result = GroupUserRepository.Get(groupUser1.Id);

            Assert.That(result, Is.EqualTo(updatedGroupUser));
        }

    }
}
