using Moderation.Entities;
using Moderation.Model;

namespace Backend.Service
{
    public interface IService
    {
        IEnumerable<Group> GetAllGroups();
        GroupUser GetGroupUserFromPostReport(PostReport report);
        GroupUser? GetGroupUserFromUserGuid(Guid userId);
        IEnumerable<JoinRequest> GetJoinRequestsForGivenGroup(Group group);
        List<TextPost> GetPostsOfAuthorsInGivenGroup(Group group);
        TextPost GetReportedPostFromReport(PostReport report);
        User GetReportedUserFromReport(PostReport report);
        IEnumerable<PostReport> GetReportsWhichBelongToGivenGroup(Group group);
        IEnumerable<JoinRequestAnswerToOneQuestion> GetRequestAnswersForGivenRequestGuid(Guid requestId);
        User? GetUserByGuid(Guid id);
        User GetUserFromGroupUser(GroupUser groupUser);
        Guid? GetUserGuidByName(string name);
    }
}