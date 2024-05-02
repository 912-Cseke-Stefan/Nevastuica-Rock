using Moderation.Entities;
using Moderation.Model;
using Moderation.Serivce;

namespace Backend.Service
{
    public class Service : IService
    {
        private ApplicationState state;

        public Service(ApplicationState state)
        {
            this.state = state;
        }

        public GroupUser GetGroupUserFromPostReport(PostReport report)
        {
            return state.GroupUsers.GetAll().Where(guser => guser.Id == report.UserId && guser.GroupId == report.GroupId).ToArray()[0];
        }

        public Guid? GetUserGuidByName(string name)
        {
            return state.UserRepository.GetGuidByName(name);
        }

        public User? GetUserByGuid(Guid id)
        {
            return state.UserRepository.Get(id);
        }

        public IEnumerable<Group> GetAllGroups()
        {
            return state.Groups.GetAll();
        }

        public List<TextPost> GetPostsOfAuthorsInGivenGroup(Group group)
        {
            return state.TextPosts.GetAll().Where(post => post.Author.GroupId == group.Id).ToList();
        }

        public IEnumerable<PostReport> GetReportsWhichBelongToGivenGroup(Group group)
        {
            return state.Reports.GetAll().Where(report => report.GroupId == group.Id);
        }

        public IEnumerable<JoinRequest> GetJoinRequestsForGivenGroup(Group group)
        {
            return state.JoinRequests.GetAll().Where(request => state.GroupUsers.Get(request.UserId)?.GroupId == group.Id);
        }

        public User GetUserFromGroupUser(GroupUser groupUser)
        {
            return state.UserRepository.GetAll().Where(user => user.Id == groupUser.UserId).ToArray()[0];
        }

        public TextPost GetReportedPostFromReport(PostReport report)
        {
            return state.TextPosts.GetAll().Where(post => post.Id == report.PostId).ToArray()[0];
        }

        public User GetReportedUserFromReport(PostReport report)
        {
            TextPost postFromReport = state.TextPosts.GetAll()
                                                     .Where(post => post.Id == report.PostId)
                                                     .ToArray()[0];
            GroupUser groupUserFromPost = state.GroupUsers.GetAll()
                                                          .Where(guser => guser.Id == postFromReport.Author.Id)
                                                          .ToArray()[0];
            return state.UserRepository.GetAll()
                                       .Where(user => user.Id == groupUserFromPost.UserId)
                                       .ToArray()[0];
        }

        public GroupUser? GetGroupUserFromUserGuid(Guid userId)
        {
            return state.GroupUsers.Get(userId);
        }

        public IEnumerable<JoinRequestAnswerToOneQuestion> GetRequestAnswersForGivenRequestGuid(Guid requestId)
        {
            return state.JoinRequestForOneQuestionAnswers.GetAll().Where(answer => answer.RequestId == requestId);
        }
    }
}
