using Backend.Repository.Interfaces;
using Moderation.DbEndpoints;
using Moderation.Entities;
using Moderation.Model;

namespace Moderation.Repository
{
    public class GroupUserRepository : IGroupUserRepository
    {
        protected readonly Dictionary<Guid, GroupUser> data;
        public GroupUserRepository(Dictionary<Guid, GroupUser> data)
        {
            this.data = data;
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
            return GroupUserEndpoints.ReadAllGroupUsers().Exists(u => u.Id == key);
        }

        public GroupUser? Get(Guid key)
        {
            return GroupUserEndpoints.ReadAllGroupUsers().Find(u => u.Id == key);
        }

        public GroupUser? GetByUserIdAndGroupId(Guid userId, Guid groupId)
        {
            return GroupUserEndpoints.ReadAllGroupUsers().Find(u => u.UserId == userId && u.GroupId == groupId);
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
