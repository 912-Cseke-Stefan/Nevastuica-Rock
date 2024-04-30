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
                new TextPost(Guid.Parse("AC60415D-2442-491D-BCA8-CBAB6A1C6555"), "Post 1!", Guid.Parse("3E7EF48E-2C84-4104-A9B1-3FC60209F692"));
            TextPost textPost2 =
                new TextPost(Guid.Parse("AC60415D-2442-491D-BCA8-CBAB6A1C6556"), "Post 2!", Guid.Parse("3E7EF48E-2C84-4104-A9B1-3FC60209F692"));
            TextPost textPost3 =
                new TextPost(Guid.Parse("AC60415D-2442-491D-BCA8-CBAB6A1C6557"), "Post 3!", Guid.Parse("3E7EF48E-2C84-4104-A9B1-3FC60209F692"));

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
                new TextPost(Guid.Parse("AC60415D-2442-491D-BCA8-CBAB6A1C6544"), "Post 1!", Guid.Parse("3E7EF48E-2C84-4104-A9B1-3FC60209F692"));

            var countBefore = repo.GetAll().ToArray().Length;
            repo.Add(textPost.Id, textPost);
            var countAfter = repo.GetAll().ToArray().Length;

            Assert.That(countAfter, Is.EqualTo(countBefore + 1));
        }

        [Test]
        public void ContainsInTextPostRepository_ExistingPost_ReturnsTrue()
        {
            TextPost textPost =
                new TextPost(Guid.Parse("AC60415D-2442-491D-BCA8-CBAB6A1C6551"), "Post 1!", Guid.Parse("3E7EF48E-2C84-4104-A9B1-3FC60209F692"));

            repo.Add(textPost.Id, textPost);

            bool result = repo.Contains(textPost.Id);

            Assert.IsTrue(result);
        }

        [Test]
        public void ContainsInTextPostRepository_NonExistingPost_ReturnsFalse()
        {
            TextPost textPost =
                new TextPost(Guid.Parse("AC60415D-2442-491D-BCA8-CBAB6A1C6552"), "Post 1!", Guid.Parse("3E7EF48E-2C84-4104-A9B1-3FC60209F692"));

            bool result = repo.Contains(textPost.Id);

            Assert.IsFalse(result);
        }

        [Test]
        public void GetInTextPostRepository_ExistingPost_ReturnsPost()
        {
            TextPost textPost =
                new TextPost(Guid.Parse("AC60415D-2442-491D-BCA8-CBAB6A1C6553"), "Post 1!", Guid.Parse("3E7EF48E-2C84-4104-A9B1-3FC60209F692"));

            repo.Add(textPost.Id, textPost);

            var result = repo.Get(textPost.Id);

            Assert.AreEqual(textPost, result);
        }

        public void GetInTextPostRepository_NonExistingPost_ReturnsNull()
        {
            TextPost textPost =
                new TextPost(Guid.Parse("AC60415D-2442-491D-BCA8-CBAB6A1C6554"), "Post 1!", Guid.Parse("3E7EF48E-2C84-4104-A9B1-3FC60209F692"));

            var result = repo.Get(textPost.Id);

            Assert.IsNull(result);
        }

        [Test]
        public void GetAllInTextPostRepository_ReturnsAllPosts()
        {
            TextPost textPost1 =
                new TextPost(Guid.Parse("AC60415D-2442-491D-BCA8-CBAB6A1C6515"), "Post 1!", Guid.Parse("3E7EF48E-2C84-4104-A9B1-3FC60209F692"));
            TextPost textPost2 =
                new TextPost(Guid.Parse("AC60415D-2442-491D-BCA8-CBAB6A1C6516"), "Post 2!", Guid.Parse("3E7EF48E-2C84-4104-A9B1-3FC60209F692"));
            TextPost textPost3 =
                new TextPost(Guid.Parse("AC60415D-2442-491D-BCA8-CBAB6A1C6517"), "Post 3!", Guid.Parse("3E7EF48E-2C84-4104-A9B1-3FC60209F692"));

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
                new TextPost(Guid.Parse("AC60415D-2442-491D-BCA8-CBAB6A1C6518"), "Post 1!", Guid.Parse("3E7EF48E-2C84-4104-A9B1-3FC60209F692"));

            repo.Add(textPost.Id, textPost);

            bool result = repo.Remove(textPost.Id);

            Assert.IsTrue(result);
        }

        [Test]
        public void UpdateInTextPostRepository_ExistingPost_ReturnsTrue()
        {
            TextPost textPost =
                new TextPost(Guid.Parse("AC60415D-2442-491D-BCA8-CBAB6A1C6519"), "Post 1!", Guid.Parse("3E7EF48E-2C84-4104-A9B1-3FC60209F692"));

            repo.Add(textPost.Id, textPost);

            bool result = repo.Update(textPost.Id, textPost);

            Assert.IsTrue(result);
        }
    }
}
