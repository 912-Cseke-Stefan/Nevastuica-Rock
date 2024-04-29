using Backend.Repository.Interfaces;
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
        public GroupRules(Dictionary<Guid, Model.Rule> data)
        {
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
