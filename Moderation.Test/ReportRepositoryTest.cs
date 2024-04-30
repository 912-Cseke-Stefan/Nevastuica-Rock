using System;
using System.Collections.Generic;
using Moderation.Repository;
using Moderation.Model;
using NUnit.Framework;

namespace Moderation.Test
{
    public class ReportRepositoryTest
    {
        private ReportRepository reportRepository;

        [SetUp]
        public void Setup()
        {
            reportRepository = new ReportRepository();
        }

        [Test]
        public void AddToReportRepository_SuccessiveAdds_ReturnsSuccessiveBool()
        {
            PostReport report1 = new PostReport(Guid.NewGuid(), Guid.NewGuid(), "Report 1", Guid.NewGuid());
            PostReport report2 = new PostReport(Guid.NewGuid(), Guid.NewGuid(), "Report 2", Guid.NewGuid());
            PostReport report3 = new PostReport(Guid.NewGuid(), Guid.NewGuid(), "Report 3", Guid.NewGuid());

            bool result1 = reportRepository.Add(report1.Id, report1);
            bool result2 = reportRepository.Add(report2.Id, report2);
            bool result3 = reportRepository.Add(report3.Id, report3);

            Assert.IsTrue(result1);
            Assert.IsTrue(result2);
            Assert.IsTrue(result3);
        }

        [Test]
        public void AddToReportRepository_NewReport_IncreasesRepositoryCountByOne()
        {
            var initialCount = reportRepository.GetAll().Count();

            PostReport report = new PostReport(Guid.NewGuid(), Guid.NewGuid(), "New Report", Guid.NewGuid());
            reportRepository.Add(report.Id, report);

            var finalCount = reportRepository.GetAll().Count();

            Assert.AreEqual(initialCount + 1, finalCount);
        }

        [Test]
        public void ContainsInReportRepository_ExistingReport_ReturnsTrue()
        {
            PostReport report = new PostReport(Guid.NewGuid(), Guid.NewGuid(), "Existing Report", Guid.NewGuid());
            reportRepository.Add(report.Id, report);

            bool result = reportRepository.Contains(report.Id);

            Assert.IsTrue(result);
        }

        [Test]
        public void ContainsInReportRepository_NonExistingReport_ReturnsFalse()
        {
            bool result = reportRepository.Contains(Guid.NewGuid());

            Assert.IsFalse(result);
        }

        [Test]
        public void GetInReportRepository_ExistingReport_ReturnsReport()
        {
            PostReport report = new PostReport(Guid.NewGuid(), Guid.NewGuid(), "Get Report", Guid.NewGuid());
            reportRepository.Add(report.Id, report);

            PostReport result = reportRepository.Get(report.Id);

            Assert.AreEqual(report, result);
        }

        [Test]
        public void GetInReportRepository_NonExistingReport_ReturnsNull()
        {
            PostReport result = reportRepository.Get(Guid.NewGuid());

            Assert.IsNull(result);
        }

        [Test]
        public void GetAllInReportRepository_ReturnsAllReports()
        {
            PostReport report1 = new PostReport(Guid.NewGuid(), Guid.NewGuid(), "Report 1", Guid.NewGuid());
            PostReport report2 = new PostReport(Guid.NewGuid(), Guid.NewGuid(), "Report 2", Guid.NewGuid());
            PostReport report3 = new PostReport(Guid.NewGuid(), Guid.NewGuid(), "Report 3", Guid.NewGuid());

            reportRepository.Add(report1.Id, report1);
            reportRepository.Add(report2.Id, report2);
            reportRepository.Add(report3.Id, report3);

            var result = reportRepository.GetAll();
            var resultArray = result.ToArray();

            Assert.Contains(report1, resultArray);
            Assert.Contains(report2, resultArray);
            Assert.Contains(report3, resultArray);
        }


        [Test]
        public void RemoveInReportRepository_ExistingReport_ReturnsTrue()
        {
            PostReport report = new PostReport(Guid.NewGuid(), Guid.NewGuid(), "Remove Report", Guid.NewGuid());
            reportRepository.Add(report.Id, report);

            bool result = reportRepository.Remove(report.Id);

            Assert.IsTrue(result);
        }



        [Test]
        public void RemoveInReportRepository_ExistingReport_DecreasesRepositoryCountByOne()
        {
            PostReport report1 = new PostReport(Guid.NewGuid(), Guid.NewGuid(), "Report 1", Guid.NewGuid());
            PostReport report2 = new PostReport(Guid.NewGuid(), Guid.NewGuid(), "Report 2", Guid.NewGuid());
            PostReport report3 = new PostReport(Guid.NewGuid(), Guid.NewGuid(), "Report 3", Guid.NewGuid());

            reportRepository.Add(report1.Id, report1);
            reportRepository.Add(report2.Id, report2);
            reportRepository.Add(report3.Id, report3);

            var result = reportRepository.GetAll();
            var resultArray = result.ToArray();
            var length = resultArray.Length;

            reportRepository.Remove(report2.Id);

            var resultAfterDelete = reportRepository.GetAll();
            var resultArrayAfterDelete = resultAfterDelete.ToArray();
            var lengthAfterDelete = resultArrayAfterDelete.Length;

            Assert.That(lengthAfterDelete, Is.EqualTo(length - 1));
        }

        [Test]
        public void RemoveInReportRepository_NonExistingReport_ReturnsTrue()
        {
            bool result = reportRepository.Remove(Guid.NewGuid());

            Assert.IsTrue(result);
        }

        [Test]
        public void UpdateInReportRepository_ExistingReport_ReturnsTrue()
        {
            PostReport report = new PostReport(Guid.NewGuid(), Guid.NewGuid(), "Update Report", Guid.NewGuid());
            reportRepository.Add(report.Id, report);
            PostReport updatedReport = new PostReport(report.Id, Guid.NewGuid(), "Updated Report", Guid.NewGuid());

            bool result = reportRepository.Update(report.Id, updatedReport);

            Assert.IsTrue(result);
        }
    }
}
