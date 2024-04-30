using Moderation.Repository;
using Moderation.Model;
using Moderation.Entities;
using NUnit.Framework;
using System;
using System.Linq;

namespace Moderation.Test
{
    public class RoleRepositoryTest
    {
        private RoleRepository roleRepository;

        [SetUp]
        public void Setup()
        {
            roleRepository = new RoleRepository();
        }

        [Test]
        public void AddToRoleRepository_SuccessiveAdds_ReturnsSuccessiveBool()
        {
            Role role1 = new Role("role1", null);
            Role role2 = new Role("role2", null);
            Role role3 = new Role("role3", null);

            bool result1 = roleRepository.Add(role1.Id, role1);
            bool result2 = roleRepository.Add(role2.Id, role2);
            bool result3 = roleRepository.Add(role3.Id, role3);

            Assert.IsTrue(result1);
            Assert.IsTrue(result2);
            Assert.IsTrue(result3);
        }

        [Test]
        public void AddToRoleRepository_NewRole_IncreasesRepositoryCountByOne()
        {
            Role role1 = new Role("role1", null);
            var result = roleRepository.GetAll();
            var resultArray = result.ToArray();
            var length = resultArray.Length;

            roleRepository.Add(role1.Id, role1);

            var resultAfterAdd = roleRepository.GetAll();
            var resultArrayAfterAdd = resultAfterAdd.ToArray();
            var lengthAfterAdd = resultArrayAfterAdd.Length;

            Assert.That(lengthAfterAdd, Is.EqualTo(length + 1));
        }

        [Test]
        public void ContainsInRoleRepository_ExistingRole_ReturnsTrue()
        {
            Role role1 = new Role("role1", null);
            Role role2 = new Role("role2", null);
            Role role3 = new Role("role3", null);

            roleRepository.Add(role1.Id, role1);
            roleRepository.Add(role2.Id, role2);
            roleRepository.Add(role3.Id, role3);

            bool result = roleRepository.Contains(role2.Id);

            Assert.IsTrue(result);
        }

        [Test]
        public void ContainsInRoleRepository_NonExistingRole_ReturnsFalse()
        {
            Role role1 = new Role("role1", null);
            Role role2 = new Role("role2", null);
            Role role3 = new Role("role3", null);

            roleRepository.Add(role1.Id, role1);
            roleRepository.Add(role2.Id, role2);
            roleRepository.Add(role3.Id, role3);

            bool result = roleRepository.Contains(Guid.NewGuid());

            Assert.IsFalse(result);
        }

        [Test]
        public void GetInRoleRepository_ExistingRole_ReturnsRole()
        {
            Role role1 = new Role("role1", null);
            Role role2 = new Role("role2", null);
            Role role3 = new Role("role3", null);

            roleRepository.Add(role1.Id, role1);
            roleRepository.Add(role2.Id, role2);
            roleRepository.Add(role3.Id, role3);

            Role result = roleRepository.Get(role2.Id);

            Assert.That(result, Is.EqualTo(role2));
        }

        [Test]
        public void GetInRoleRepository_NonExistingRole_ReturnsNull()
        {
            Role role1 = new Role("role1", null);
            Role role2 = new Role("role2", null);
            Role role3 = new Role("role3", null);

            roleRepository.Add(role1.Id, role1);
            roleRepository.Add(role2.Id, role2);
            roleRepository.Add(role3.Id, role3);

            Role result = roleRepository.Get(Guid.NewGuid());

            Assert.IsNull(result);
        }

        [Test]
        public void GetAllInRoleRepository_ReturnsAllRoles()
        {
            Role role1 = new Role("role1", null);
            Role role2 = new Role("role2", null);
            Role role3 = new Role("role3", null);

            roleRepository.Add(role1.Id, role1);
            roleRepository.Add(role2.Id, role2);
            roleRepository.Add(role3.Id, role3);

            var result = roleRepository.GetAll();
            var resultArray = result.ToArray();

            Assert.Contains(role1, resultArray);
            Assert.Contains(role2, resultArray);
            Assert.Contains(role3, resultArray);
        }

        [Test]
        public void RemoveInRoleRepository_ExistingRole_ReturnsTrue()
        {
            Role role1 = new Role("role1", null);
            Role role2 = new Role("role2", null);
            Role role3 = new Role("role3", null);

            roleRepository.Add(role1.Id, role1);
            roleRepository.Add(role2.Id, role2);
            roleRepository.Add(role3.Id, role3);

            bool result = roleRepository.Remove(role2.Id);

            Assert.IsTrue(result);
        }

        [Test]
        public void RemoveInRoleRepository_ExistingRole_DecreasesRepositoryCountByOne()
        {
            Role role1 = new Role("role1", null);
            Role role2 = new Role("role2", null);
            Role role3 = new Role("role3", null);

            roleRepository.Add(role1.Id, role1);
            roleRepository.Add(role2.Id, role2);
            roleRepository.Add(role3.Id, role3);

            var result = roleRepository.GetAll();
            var resultArray = result.ToArray();
            var length = resultArray.Length;

            roleRepository.Remove(role2.Id);

            var resultAfterDelete = roleRepository.GetAll();
            var resultArrayAfterDelete = resultAfterDelete.ToArray();
            var lengthAfterDelete = resultArrayAfterDelete.Length;

            Assert.That(lengthAfterDelete, Is.EqualTo(length - 1));
        }

        [Test]
        public void RemoveInRoleRepository_NonExistingRole_ReturnsTrue()
        {
            Role role1 = new Role("role1", null);
            Role role2 = new Role("role2", null);
            Role role3 = new Role("role3", null);

            roleRepository.Add(role1.Id, role1);
            roleRepository.Add(role2.Id, role2);
            roleRepository.Add(role3.Id, role3);

            bool result = roleRepository.Remove(Guid.NewGuid());

            Assert.IsTrue(result);
        }

        [Test]
        public void UpdateInRoleRepository_ExistingRole_ReturnsTrue()
        {
            Role role1 = new Role("role1", null);
            Role role2 = new Role("role2", null);
            Role role3 = new Role("role3", null);

            roleRepository.Add(role1.Id, role1);
            roleRepository.Add(role2.Id, role2);
            roleRepository.Add(role3.Id, role3);

            Role updatedRole2 = new Role("role2New", null);
            updatedRole2.Id = role2.Id;

            bool result = roleRepository.Update(role2.Id, updatedRole2);

            Assert.IsTrue(result);
        }

        [Test]
        public void UpdateInRoleRepository_ExistingRole_UpdatesRoleInRepository()
        {
            Role role1 = new Role("role1", null);
            Role role2 = new Role("role2", null);
            Role role3 = new Role("role3", null);

            roleRepository.Add(role1.Id, role1);
            roleRepository.Add(role2.Id, role2);
            roleRepository.Add(role3.Id, role3);

            Role updatedRole2 = new Role("role2New", null);
            updatedRole2.Id = role2.Id;

            roleRepository.Update(role2.Id, updatedRole2);

            var result = roleRepository.Get(role2.Id);

            Assert.AreEqual(updatedRole2.Name, result.Name); 
            Assert.AreEqual(updatedRole2.Permissions, result.Permissions); 
        }
    }
}

