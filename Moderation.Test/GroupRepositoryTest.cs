using Moderation.Repository;
using Moderation.Model;
using Moderation.Entities;
using NUnit.Framework;

namespace Moderation.Test
{
    public class GroupRepositoryTest
    {
        private GroupRepository GroupRepository;

        [SetUp]
        public void Setup()
        {
            GroupRepository = new GroupRepository();
        }

        [Test]
        public void AddToGroupRepository_SuccessiveAdds_ReturnsSuccessiveBool()
        {
            Group group1 = new Group("group1", "description1", new User("user1"));
            Group group2 = new Group("group2", "description2", new User("user2"));
            Group group3 = new Group("group3", "description3", new User("user3"));


            bool result1 = GroupRepository.Add(group1.Id, group1);
            bool result2 = GroupRepository.Add(group2.Id, group2);
            bool result3 = GroupRepository.Add(group3.Id, group3);

            Assert.IsTrue(result1);
            Assert.IsTrue(result2);
            Assert.IsTrue(result3);
        }

        [Test]
        public void AddToGroupRepository_NewGroup_IncreasesRepositoryCountByOne()
        {
            Group group1 = new Group("group1", "description1", new User("user1"));
            var result = GroupRepository.GetAll();
            var resultArray = result.ToArray();
            var length = resultArray.Length;

            GroupRepository.Add(group1.Id, group1);

            var resultAfterAdd = GroupRepository.GetAll();
            var resultArrayAfterAdd = resultAfterAdd.ToArray();
            var lengthAfterAdd = resultArrayAfterAdd.Length;

            Assert.That(lengthAfterAdd, Is.EqualTo(length + 1));

        }

        [Test]
        public void ContainsInGroupRepository_ExistingGroup_ReturnsTrue()
        {
            Group group1 = new Group("group1", "description1", new User("user1"));
            Group group2 = new Group("group2", "description2", new User("user2"));
            Group group3 = new Group("group3", "description3", new User("user3"));

            GroupRepository.Add(group1.Id, group1);
            GroupRepository.Add(group2.Id, group2);
            GroupRepository.Add(group3.Id, group3);

            bool result = GroupRepository.Contains(group2.Id);

            Assert.IsTrue(result);
        }

        [Test]
        public void ContainsInGroupRepository_NonExistingGroup_ReturnsFalse()
        {
            Group group1 = new Group("group1", "description1", new User("user1"));
            Group group2 = new Group("group2", "description2", new User("user2"));
            Group group3 = new Group("group3", "description3", new User("user3"));

            GroupRepository.Add(group1.Id, group1);
            GroupRepository.Add(group2.Id, group2);
            GroupRepository.Add(group3.Id, group3);

            bool result = GroupRepository.Contains(Guid.NewGuid());

            Assert.IsFalse(result);
        }

        [Test]
        public void GetInGroupRepository_ExistingGroup_ReturnsGroup()
        {
            Group group1 = new Group("group1", "description1", new User("user1"));
            Group group2 = new Group("group2", "description2", new User("user2"));
            Group group3 = new Group("group3", "description3", new User("user3"));

            GroupRepository.Add(group1.Id, group1);
            GroupRepository.Add(group2.Id, group2);
            GroupRepository.Add(group3.Id, group3);

            Group result = GroupRepository.Get(group2.Id);

            Assert.That(result, Is.EqualTo(group2));
        }

        [Test]
        public void GetInGroupRepository_NonExistingGroup_ReturnsNull()
        {
            Group group1 = new Group("group1", "description1", new User("user1"));
            Group group2 = new Group("group2", "description2", new User("user2"));
            Group group3 = new Group("group3", "description3", new User("user3"));

            GroupRepository.Add(group1.Id, group1);
            GroupRepository.Add(group2.Id, group2);
            GroupRepository.Add(group3.Id, group3);

            Group? result = GroupRepository.Get(Guid.NewGuid());

            Assert.IsNull(result);
        }

        [Test]
        public void GetAllInGroupRepository_ReturnsAllGroups()
        {
            Group group1 = new Group("group1", "description1", new User("user1"));
            Group group2 = new Group("group2", "description2", new User("user2"));
            Group group3 = new Group("group3", "description3", new User("user3"));

            GroupRepository.Add(group1.Id, group1);
            GroupRepository.Add(group2.Id, group2);
            GroupRepository.Add(group3.Id, group3);

            var result = GroupRepository.GetAll();
            var resultArray = result.ToArray();

            Assert.Contains(group1, resultArray);
            Assert.Contains(group2, resultArray);
            Assert.Contains(group3, resultArray);
        }

        [Test]
        public void RemoveInGroupRepository_ExistingGroup_ReturnsTrue()
        {
            Group group1 = new Group("group1", "description1", new User("user1"));
            Group group2 = new Group("group2", "description2", new User("user2"));
            Group group3 = new Group("group3", "description3", new User("user3"));

            GroupRepository.Add(group1.Id, group1);
            GroupRepository.Add(group2.Id, group2);
            GroupRepository.Add(group3.Id, group3);

            bool result = GroupRepository.Remove(group2.Id);

            Assert.IsTrue(result);
        }

        [Test]
        public void RemoveInGroupRepository_ExistingGroup_DecreasesRepositoryCountByOne()
        {
            Group group1 = new Group("group1", "description1", new User("user1"));
            Group group2 = new Group("group2", "description2", new User("user2"));
            Group group3 = new Group("group3", "description3", new User("user3"));

            GroupRepository.Add(group1.Id, group1);
            GroupRepository.Add(group2.Id, group2);
            GroupRepository.Add(group3.Id, group3);

            var result = GroupRepository.GetAll();
            var resultArray = result.ToArray();
            var length = resultArray.Length;

            GroupRepository.Remove(group2.Id);

            var resultAfterDelete = GroupRepository.GetAll();
            var resultArrayAfterDelete = resultAfterDelete.ToArray();
            var lengthAfterDelete = resultArrayAfterDelete.Length;

            Assert.That(lengthAfterDelete, Is.EqualTo(length - 1));
        }

        [Test]
        public void RemoveInGroupRepository_NonExistingGroup_ReturnsTrue()
        {
            Group group1 = new Group("group1", "description1", new User("user1"));
            Group group2 = new Group("group2", "description2", new User("user2"));
            Group group3 = new Group("group3", "description3", new User("user3"));

            GroupRepository.Add(group1.Id, group1);
            GroupRepository.Add(group2.Id, group2);
            GroupRepository.Add(group3.Id, group3);

            bool result = GroupRepository.Remove(Guid.NewGuid());

            Assert.IsTrue(result);
        }

        [Test]
        public void UpdateInGroupRepository_ExistingGroup_ReturnsTrue()
        {
            Group group1 = new Group("group1", "description1", new User("user1"));
            Group group2 = new Group("group2", "description2", new User("user2"));
            Group group3 = new Group("group3", "description3", new User("user3"));

            GroupRepository.Add(group1.Id, group1);
            GroupRepository.Add(group2.Id, group2);
            GroupRepository.Add(group3.Id, group3);

            Group updatedGroup2 = new Group("group2New", "description2", new User("user2"));
            updatedGroup2.Id = group2.Id;

            bool result = GroupRepository.Update(group2.Id, updatedGroup2);

            Assert.IsTrue(result);
        }

        [Test]
        public void UpdateInGroupRepository_ExistingGroup_UpdatesGroupInRepository()
        {
            Group group1 = new Group("group1", "description1", new User("user1"));
            Group group2 = new Group("group2", "description2", new User("user2"));
            Group group3 = new Group("group3", "description3", new User("user3"));

            GroupRepository.Add(group1.Id, group1);
            GroupRepository.Add(group2.Id, group2);
            GroupRepository.Add(group3.Id, group3);

            Group updatedGroup2 = new Group("group2New", "description2", new User("user2"));
            updatedGroup2.Id = group2.Id;

            GroupRepository.Update(group2.Id, updatedGroup2);

            var result = GroupRepository.Get(group2.Id);

            Assert.That(result, Is.EqualTo(updatedGroup2));
        }

        [Test]
        public void GetGuidByNameInGroupRepository_ExistingGroup_ReturnsGuid()
        {
            Group group1 = new Group("group1000", "description1", new User("user1"));

            GroupRepository.Add(group1.Id, group1);

            Guid? result = GroupRepository.GetGuidByName("group1000");

            Assert.That(result, Is.EqualTo(group1.Id));
        }

        [Test]
        public void GetGuidByNameInGroupRepository_NonExistingGroup_ReturnsNull()
        {
            Group group1 = new Group("group1", "description1", new User("user1"));
            Group group2 = new Group("group2", "description2", new User("user2"));
            Group group3 = new Group("group3", "description3", new User("user3"));

            GroupRepository.Add(group1.Id, group1);
            GroupRepository.Add(group2.Id, group2);
            GroupRepository.Add(group3.Id, group3);

            Guid? result = GroupRepository.GetGuidByName("group4");

            Assert.IsNull(result);
        }
    }
}
