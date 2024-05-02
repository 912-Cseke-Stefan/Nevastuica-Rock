using Moderation.Repository;
using Moderation.Model;
using Moderation.Entities;

namespace Moderation.Test
{
    public class AwardRepositoryTest
    {
        private AwardRepository repo;

        [SetUp]
        public void Setup()
        {
            repo = new AwardRepository();
        }

        [Test]
        public void AddToAwardRepository_SuccessiveAdds_ReturnsSuccessiveBool()
        {
            Award award1 = new Award(Guid.Parse("AC60415D-2442-491D-BCA8-CBAB6A1C6555"), (Award.AwardType)1);
            Award award2 = new Award(Guid.Parse("AC60415D-2442-491D-BCA8-CBAB6A1C6556"), (Award.AwardType)1);
            Award award3 = new Award(Guid.Parse("AC60415D-2442-491D-BCA8-CBAB6A1C6557"), (Award.AwardType)1);

            bool result1 = repo.Add(award1.Id, award1);
            bool result2 = repo.Add(award2.Id, award2);
            bool result3 = repo.Add(award3.Id, award3);

            Assert.IsTrue(result1);
            Assert.IsTrue(result2);
            Assert.IsTrue(result3);
        }

        [Test]
        public void AddToAwardRepository_NewAward_IncreasesCountByOne()
        {
            Award award = new Award(Guid.Parse("AC60415D-2442-491D-BCA8-CBAB6A1C6544"), (Award.AwardType)1);

            var countBefore = repo.GetAll().ToArray().Length;
            repo.Add(award.Id, award);
            var countAfter = repo.GetAll().ToArray().Length;

            Assert.That(countAfter, Is.EqualTo(countBefore + 1));
        }

        [Test]
        public void ContainsInAwardRepository_ExistingAward_ReturnsTrue()
        {
            Award award = new Award(Guid.Parse("AC60415D-2442-491D-BCA8-CBAB6A1C6551"), (Award.AwardType)1);

            repo.Add(award.Id, award);

            bool result = repo.Contains(award.Id);

            Assert.IsTrue(result);
        }

        [Test]
        public void GetInAwardRepository_ExistingAward_ReturnsAward()
        {
            Award award = new Award(Guid.Parse("AC60415D-2442-491D-BCA8-CBAB6A1C6522"), (Award.AwardType)1);

            repo.Add(award.Id, award);

            var result = repo.Get(award.Id);

            Assert.That(result, Is.EqualTo(award));
        }

        [Test]
        public void GetInAwardRepository_NonExistingAward_ReturnsNull()
        {
            Award award = new Award(Guid.Parse("AC60415D-2442-491D-BCA8-22AB6A1C6551"), (Award.AwardType)1);

            var result = repo.Get(award.Id);

            Assert.IsNull(result);
        }

        [Test]
        public void RemoveInAwardRepository_ExistingAward_ReturnsTrue()
        {
            Award award = new Award(Guid.Parse("AC60415D-2442-491D-BCA8-CBAB6A1C1551"), (Award.AwardType)1);

            repo.Add(award.Id, award);

            bool result = repo.Remove(award.Id);

            Assert.IsTrue(result);
        }

        [Test]
        public void UpdateInAwardRepository_ExistingAward_ReturnsTrue()
        {
            Award award = new Award(Guid.Parse("AC60415D-2442-491D-BCA8-CBAB6A4C6551"), (Award.AwardType)1);

            repo.Add(award.Id, award);

            bool result = repo.Update(award.Id, award);

            Assert.IsTrue(result);
        }

        [Test]
        public void GetAllInAwardRepository_ReturnsAllAwards()
        {
            Award award1 = new Award(Guid.Parse("AC60415D-2442-491D-BCA8-CBAB651C6551"), (Award.AwardType)1);
            Award award2 = new Award(Guid.Parse("AC60415D-2442-491D-BCA8-CBAB651C6552"), (Award.AwardType)1);
            Award award3 = new Award(Guid.Parse("AC60415D-2442-491D-BCA8-CBAB651C6553"), (Award.AwardType)1);

            repo.Add(award1.Id, award1);
            repo.Add(award2.Id, award2);
            repo.Add(award3.Id, award3);

            var result = repo.GetAll().ToArray();

            Assert.Contains(award1, result);
            Assert.Contains(award2, result);
            Assert.Contains(award3, result);
        }
    }
}
