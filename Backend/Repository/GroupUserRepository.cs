using Backend.Repository.Interfaces;
using Moderation.DbEndpoints;
using Moderation.Entities;

namespace Moderation.Repository
{
    public class GroupUserRepository : IGroupUserRepository
    {
        protected readonly Dictionary<Guid, GroupUser> Data;
        public GroupUserRepository(Dictionary<Guid, GroupUser> data)
        {
            this.Data = data;
        }
        public GroupUserRepository() : base()
        {
        }
        public bool Add(Guid key, GroupUser value)
        {
            GroupUserEndpoints.CreateGroupUser(value);
            return true;
        }
        public bool Contains(Guid key)
        {
            return GroupUserEndpoints.ReadAllGroupUsers().Exists(groupUser => groupUser.Id == key);
        }

        public GroupUser? Get(Guid key)
        {
            return GroupUserEndpoints.ReadAllGroupUsers().Find(groupUser => groupUser.Id == key);
        }

        public GroupUser? GetByUserIdAndGroupId(Guid userId, Guid groupId)
        {
            return GroupUserEndpoints.ReadAllGroupUsers().Find(groupUser => groupUser.UserId == userId && groupUser.GroupId == groupId);
        }

        public IEnumerable<GroupUser> GetAll()
        {
            return GroupUserEndpoints.ReadAllGroupUsers();
        }

        public bool Remove(Guid key)
        {
            GroupUserEndpoints.DeleteGroupUser(key);
            return true;
        }
        public bool Update(Guid key, GroupUser value)
        {
            GroupUserEndpoints.UpdateGroupUser(value);
            return true;
        }
    }
}
