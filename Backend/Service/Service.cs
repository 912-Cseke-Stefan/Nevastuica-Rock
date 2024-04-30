using Moderation.Entities;
using Moderation.Model;
using Moderation.Serivce;

namespace Backend.Service
{
    public class Service
    {
        private ApplicationState state;

        public Service(ApplicationState state)
        {
            this.state = state;
        }

        public GroupUser GetGroupUsersByIdAndUserId(PostReport report)
        {
            return state.GroupUsers.GetAll().Where(guser => guser.Id == report.UserId && guser.GroupId == report.GroupId).ToArray()[0];
        }
    }
}
