using NUnit.Framework;
using Moderation.Entities;
using Moderation.Repository;
using System;
using System.Collections.Generic;

namespace Moderation.Test
{
    public class JoinRequestRepositoryTests
    {
        private JoinRequestRepository repository;

        [SetUp]
        public void Setup()
        {
            repository = new JoinRequestRepository();
        }

        private JoinRequest CreateJoinRequest()
        {
            return new JoinRequest(Guid.NewGuid());
        }

        [Test]
        public void Add_SuccessfullyAddsJoinRequest()
        {
            var joinRequest = CreateJoinRequest();
            repository.Add(joinRequest.Id, joinRequest);
            Assert.IsTrue(repository.Contains(joinRequest.Id));
        }

        [Test]
        public void Contains_ReturnsTrueWhenJoinRequestExists()
        {
            var joinRequest = CreateJoinRequest();
            repository.Add(joinRequest.Id, joinRequest);
            var result = repository.Contains(joinRequest.Id);
            Assert.IsTrue(result);
        }

        [Test]
        public void Contains_ReturnsFalseWhenJoinRequestDoesNotExist()
        {
            var nonExistentId = Guid.NewGuid();
            var result = repository.Contains(nonExistentId);
            Assert.IsFalse(result);
        }

        [Test]
        public void Get_ReturnsJoinRequestWhenItExists()
        {
            var joinRequest = CreateJoinRequest();
            repository.Add(joinRequest.Id, joinRequest);
            var result = repository.Get(joinRequest.Id);
            Assert.That(result, Is.EqualTo(joinRequest));
        }

        [Test]
        public void Get_ReturnsNullWhenJoinRequestDoesNotExist()
        {
            var nonExistentId = Guid.NewGuid();
            var result = repository.Get(nonExistentId);
            Assert.IsNull(result);
        }

        [Test]
        public void GetAll_ReturnsAllJoinRequests()
        {
            var joinRequest1 = CreateJoinRequest();
            var joinRequest2 = CreateJoinRequest();
            repository.Add(joinRequest1.Id, joinRequest1);
            repository.Add(joinRequest2.Id, joinRequest2);
            var result = repository.GetAll();
            CollectionAssert.Contains(result, joinRequest1);
            CollectionAssert.Contains(result, joinRequest2);
        }

        [Test]
        public void Remove_SuccessfullyRemovesJoinRequest()
        {
            var joinRequest = CreateJoinRequest();
            repository.Add(joinRequest.Id, joinRequest);
            repository.Remove(joinRequest.Id);
            Assert.IsFalse(repository.Contains(joinRequest.Id));
        }
    }
}
