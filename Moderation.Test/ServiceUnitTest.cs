using Backend.Repository.Interfaces;
using Backend.Service;
using Moderation.Entities;
using Moderation.Model;
using Moderation.Serivce;
using Moderation.Test.Mocks;

namespace Moderation.Test
{
    internal class ServiceUnitTest
    {
        private Service service;

        [SetUp]
        public void Setup()
        {
            IGroupRepository Groups = new MockGroupRepository();
            IUserRepository UserRepository = new MockUserRepository();
            IPostRepository Posts = new MockPostRepository();
            IAwardRepository Awards = new MockAwardRepository();
            IGroupRules Rules = new MockGroupRules();
            IGroupUserRepository GroupUsers = new MockGroupUserRepository();
            IJoinRequestAnswerForOneQuestionRepository JoinRequestForOneQuestionAnswers = new MockJoinRequestAnswerForOneQuestionRepository();
            IJoinRequestRepository JoinRequests = new MockJoinRequestRepository();
            IQuestionRepository Questions = new MockQuestionRepository();
            IReportRepository Reports = new MockReportRepository();
            IRoleRepository Roles = new MockRoleRepository();
            ITextPostRepository TextPosts = new MockTextPostRepository();
            ApplicationState mockApplicationState = new ApplicationState(Groups,
                                                                         UserRepository,
                                                                         Posts,
                                                                         Awards,
                                                                         Rules,
                                                                         GroupUsers,
                                                                         JoinRequestForOneQuestionAnswers,
                                                                         JoinRequests,
                                                                         Questions,
                                                                         Reports,
                                                                         Roles,
                                                                         TextPosts);
            service = new Service(mockApplicationState);
        }

        
        public void GetGroupUserFromPostReport_CorrectPost_ReturnsGroupUser()
        {
            PostReport report = new PostReport(Guid.Parse("B7CCB450-EE32-4BFF-8383-E0A0F36CAC06"), Guid.NewGuid(), "post", Guid.Parse("3E0F1ED0-8EAF-4D71-AFC7-07D62FFEF973"));

            GroupUser groupUser = service.GetGroupUserFromPostReport(report);

            Assert.That(groupUser.UserId, Is.EqualTo(Guid.Parse("B7CCB450-EE32-4BFF-8383-E0A0F36CAC06")));
        }

        [Test]
        public void GetUserGuidByName_GiveName_ReturnGuid()
        {
            string searchedName = "user";

            Guid? guidOfSearchedName = service.GetUserGuidByName(searchedName);

            Assert.That(guidOfSearchedName, Is.EqualTo(Guid.Parse("B05ABC1A-8952-41FB-A503-BFAD23CA9092")));
        }

        [Test]
        public void GetUserByGuid_GivenGuid_ReturnsTestUser()
        {
            Guid searchedGuid = Guid.Parse("B05ABC1A-8952-5555-A503-BFAD23CA9092");

            User? testUser = service.GetUserByGuid(searchedGuid);

            Assert.That(testUser.Username, Is.EqualTo("TestUser"));
        }

        [Test]
        public void GetAllGroups_NoParameters_ReturnsTwoElements()
        {
            IEnumerable<Group> allElementsInGroupRepo = service.GetAllGroups();

            Assert.That(allElementsInGroupRepo.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GetPostsOfAuthorsInGivenGroup_WrongGroup_ReturnsNoPosts()
        {
            Group wrongGroup = new Group(
                    Guid.Parse("3E0F1ED0-8EAF-9999-9999-07D62FFEF973"),
                    "Victor's study group",
                    "none provided",
                    new User("Victor"));

            List<TextPost> emptyPostsList = service.GetPostsOfAuthorsInGivenGroup(wrongGroup);

            Assert.That(emptyPostsList.Count(), Is.EqualTo(0));
        }

        [Test]
        public void GetReportsWhichBelongToGivenGroup_ProvideGroup_ReturnCorrectNumberOfReports()
        {
            Group groupToSearchFor = new Group(
                    Guid.Parse("3E0F1ED0-8EAF-4D71-AFC7-07D62FFEF973"),
                    "Victor's study group",
                    "none provided",
                    new User("Victor"));

            IEnumerable<PostReport> reportsByUsersInTheGivenGroup = service.GetReportsWhichBelongToGivenGroup(groupToSearchFor);

            Assert.That(reportsByUsersInTheGivenGroup.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GetJoinRequestsForGivenGroup_ProvideGroup_ReturnCorrectNumberOfReports()
        {
            Group groupToSearchFor = new Group(
                    Guid.Parse("3E0F1ED0-8EAF-4D71-AFC7-07D62FFEF973"),
                    "Victor's study group",
                    "none provided",
                    new User("Victor"));

            IEnumerable<JoinRequest> requestsToJoinAGivenGroup = service.GetJoinRequestsForGivenGroup(groupToSearchFor);

            Assert.That(requestsToJoinAGivenGroup.Count(), Is.EqualTo(3));
        }

        [Test]
        public void GetUserFromGroupUser_GivenGroupUser_ReturnCorrectUser()
        {
            GroupUser riggedGroupUser = new GroupUser(
                    Guid.Parse("B05ABC1A-8952-41FB-A503-BFAD23CA9092"),
                /*User*/Guid.Parse("B7CCB450-EE32-4BFF-8383-E0A0F36CAC06"),   // victor
                /*Group*/Guid.Parse("3E0F1ED0-8EAF-4D71-AFC7-07D62FFEF973"), // victor's study group
                /*Post score*/          1,
                /*Marketplace Score*/   1,
                new UserStatus(UserRestriction.None, DateTime.Now),
                /*Role*/Guid.Parse("00E25F4D-6C60-456B-92CF-D37751176177"));

            User userOfTheGivenGroupUserObject = service.GetUserFromGroupUser(riggedGroupUser);

            Assert.That(userOfTheGivenGroupUserObject.Id, Is.EqualTo(Guid.Parse("B7CCB450-EE32-4BFF-8383-E0A0F36CAC06")));
        }

        [Test]
        public void GetReportedPostFromReport_ReportGiven_ReturnsCorrectPost()
        {
            PostReport theGivenPostReport = new PostReport(new Guid(), Guid.Parse("2077F417-CB31-4728-B5BB-3AA57239BBCD"), "message3", Guid.Parse("3E0F1ED0-8EAF-9999-9999-07D62FFEF973"));

            TextPost theReportedPost = service.GetReportedPostFromReport(theGivenPostReport);

            Assert.That(theReportedPost.Content, Is.EqualTo("content1"));
        }
    }
}
