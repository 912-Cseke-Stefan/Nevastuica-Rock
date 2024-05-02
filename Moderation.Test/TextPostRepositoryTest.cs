using Moderation.Repository;
using Moderation.Model;
using Moderation.Entities;
using NUnit.Framework;

namespace Moderation.Test
{
    public class TextPostRepositoryTest
    {
        private TextPostRepository repo;

        [SetUp]
        public void Setup()
        {
            repo = new TextPostRepository();
        }

        [Test]
        public void AddToTextPostRepository_SuccessiveAdds_ReturnsSuccessiveBool()
        {
            TextPost textPost1 =
                new TextPost("Post 1!", new GroupUser(Guid.NewGuid(), Guid.NewGuid()));
            TextPost textPost2 =
                new TextPost("Post 2!", new GroupUser(Guid.NewGuid(), Guid.NewGuid()));
            TextPost textPost3 =
                new TextPost("Post 3!", new GroupUser(Guid.NewGuid(), Guid.NewGuid()));

            bool result1 = repo.Add(textPost1.Id, textPost1);
            bool result2 = repo.Add(textPost2.Id, textPost2);
            bool result3 = repo.Add(textPost3.Id, textPost3);

            Assert.IsTrue(result1);
            Assert.IsTrue(result2);
            Assert.IsTrue(result3);
        }

        [Test]
        public void AddToTextPostRepository_NewPost_IncreasesCountByOne()
        {
            TextPost textPost =
                new TextPost("Post 1!", new GroupUser(Guid.NewGuid(), Guid.NewGuid()));

            var countBefore = repo.GetAll().ToArray().Length;
            repo.Add(textPost.Id, textPost);
            var countAfter = repo.GetAll().ToArray().Length;

            Assert.That(countAfter, Is.EqualTo(countBefore + 1));
        }

        [Test]
        public void ContainsInTextPostRepository_ExistingPost_ReturnsTrue()
        {
            TextPost textPost =
                new TextPost("Post 1!", new GroupUser(Guid.NewGuid(), Guid.NewGuid()));

            repo.Add(textPost.Id, textPost);

            bool result = repo.Contains(textPost.Id);

            Assert.IsTrue(result);
        }

        [Test]
        public void ContainsInTextPostRepository_NonExistingPost_ReturnsFalse()
        {
            TextPost textPost =
                new TextPost("Post 1!", new GroupUser(Guid.NewGuid(), Guid.NewGuid()));

            bool result = repo.Contains(textPost.Id);

            Assert.IsFalse(result);
        }

        [Test]
        public void GetInTextPostRepository_ExistingPost_ReturnsPost()
        {
            TextPost textPost =
                new TextPost("Post 1!", new GroupUser(Guid.NewGuid(), Guid.NewGuid()));

            repo.Add(textPost.Id, textPost);

            var result = repo.Get(textPost.Id);

            Assert.AreEqual(textPost, result);
        }

        public void GetInTextPostRepository_NonExistingPost_ReturnsNull()
        {
            TextPost textPost =
                new TextPost("Post 1!", new GroupUser(Guid.NewGuid(), Guid.NewGuid()));

            var result = repo.Get(textPost.Id);

            Assert.IsNull(result);
        }

        [Test]
        public void GetAllInTextPostRepository_ReturnsAllPosts()
        {
            TextPost textPost1 =
                new TextPost("Post 1!", new GroupUser(Guid.NewGuid(), Guid.NewGuid()));
            TextPost textPost2 =
                new TextPost("Post 2!", new GroupUser(Guid.NewGuid(), Guid.NewGuid()));
            TextPost textPost3 =
                new TextPost("Post 3!", new GroupUser(Guid.NewGuid(), Guid.NewGuid()));

            repo.Add(textPost1.Id, textPost1);
            repo.Add(textPost2.Id, textPost2);
            repo.Add(textPost3.Id, textPost3);

            var result = repo.GetAll().ToArray();

            Assert.Contains(textPost1, result);
            Assert.Contains(textPost2, result);
            Assert.Contains(textPost3, result);
        }

        [Test]
        public void RemoveInTextPostRepository_ExistingPost_ReturnsTrue()
        {
            TextPost textPost =
                new TextPost("Post 1!", new GroupUser(Guid.NewGuid(), Guid.NewGuid()));

            repo.Add(textPost.Id, textPost);

            bool result = repo.Remove(textPost.Id);

            Assert.IsTrue(result);
        }

        [Test]
        public void UpdateInTextPostRepository_ExistingPost_ReturnsTrue()
        {
            TextPost textPost =
                new TextPost("Post 1!", new GroupUser(Guid.NewGuid(), Guid.NewGuid()));

            repo.Add(textPost.Id, textPost);

            bool result = repo.Update(textPost.Id, textPost);

            Assert.IsTrue(result);
        }
    }
}
