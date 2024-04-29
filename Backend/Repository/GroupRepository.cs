﻿using Backend.Repository.Interfaces;
using Moderation.DbEndpoints;
using Moderation.Entities;
using Moderation.Model;

namespace Moderation.Repository
{
    public class GroupRepository : IGroupRepository
    {
        protected readonly Dictionary<Guid, Group> data;
        public GroupRepository(Dictionary<Guid, Group> data)
        {
            this.data = data;
        }

        public GroupRepository() : base()
        {
        }

        public bool Add(Guid key, Group value)
        {
            GroupEndpoints.CreateGroup(value);
            return true;
        }

        public bool Contains(Guid key)
        {
            return GroupEndpoints.ReadAllGroups().Exists(u => u.Id == key);
        }

        public  Group? Get(Guid key)
        {
            return GroupEndpoints.ReadAllGroups().Find(u => u.Id == key);
        }

        public IEnumerable<Group> GetAll()
        {
            return GroupEndpoints.ReadAllGroups();
        }

        public bool Remove(Guid key)
        {
            GroupEndpoints.DeleteGroup(key);
            return true;
        }

        public bool Update(Guid key, Group value)
        {
            GroupEndpoints.UpdateGroup(value);
            return true;
        }
        public Guid? GetGuidByName(string name)
        {
            return GroupEndpoints.ReadAllGroups().Find(u => u.Name == name)?.Id;
        }
    }
}
