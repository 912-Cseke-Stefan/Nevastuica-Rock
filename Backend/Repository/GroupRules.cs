using Backend.Repository.Interfaces;
using Moderation.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moderation.Repository
{
    public class GroupRules : IGroupRules
    {
        protected readonly Dictionary<Guid, Rule> data;
        public GroupRules(Dictionary<Guid, Rule> data)
        {
            this.data = data;
        }
        public GroupRules() : base()
        {
        }

        // public IEnumerable<GroupRules> GetGroupRulesByGroup(Guid groupId)
        // {
        //    return data.Values.Where(q => q.GroupId == groupId);
        // }
    }
}
