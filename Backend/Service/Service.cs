using Moderation.Entities;
using Moderation.Model;
using Moderation.Serivce;

namespace Backend.Service
{
    public class Service
    {
        private static Service instance;
        private ApplicationState state;

        public Service(ApplicationState state)
        {
            this.state = state;
            instance = this;
        }

        public static Service GetService()
        {
            return instance;
        }

        public GroupUser GetGroupUserFromPostReport(PostReport report)
        {
            return state.GroupUsers.GetAll().Where(guser => guser.Id == report.UserId && guser.GroupId == report.GroupId).ToArray()[0];
        }

        public Guid? GetUserGuidByName(string name)
        {
            return state.UserRepository.GetGuidByName(name);
        }

        public User GetUserByGuid(Guid id)
        {
            return state.UserRepository.Get(id);
        }
    }
}
