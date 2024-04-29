﻿using Moderation.DbEndpoints;
using Moderation.Entities;

namespace Moderation.Repository
{
    public class GroupUserRepository : Repository<GroupUser>
    {
        public GroupUserRepository(Dictionary<Guid, GroupUser> data) : base(data)
        {
        }
        public GroupUserRepository() : base()
        {
        }
        public override bool Add(Guid key, GroupUser value)
        {
            GroupUserEndpoints.CreateGroupUser(value);
            return true;
        }
        public override bool Contains(Guid key)
        {
            return GroupUserEndpoints.ReadAllGroupUsers().Exists(u => u.Id == key);
        }

        public override GroupUser? Get(Guid key)
        {
            return GroupUserEndpoints.ReadAllGroupUsers().Find(u => u.Id == key);
        }

        public GroupUser? GetByUserIdAndGroupId(Guid userId, Guid groupId)
        {
            return GroupUserEndpoints.ReadAllGroupUsers().Find(u => u.UserId == userId && u.GroupId == groupId);
        }

        public override IEnumerable<GroupUser> GetAll()
        {
            return GroupUserEndpoints.ReadAllGroupUsers();
        }

        public override bool Remove(Guid key)
        {
            GroupUserEndpoints.DeleteGroupUser(key);
            return true;
        }
        public override bool Update(Guid key, GroupUser value)
        {
            GroupUserEndpoints.UpdateGroupUser(value);
            return true;
        }
    }
}
