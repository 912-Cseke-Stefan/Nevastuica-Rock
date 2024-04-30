using Backend.Repository.Interfaces;
using Moderation.DbEndpoints;
using Moderation.Entities;

namespace Moderation.Repository
{
    public class RoleRepository : IRoleRepository
    {
        protected readonly Dictionary<Guid, Role> Data;
        public RoleRepository(Dictionary<Guid, Role> data)
        {
            this.Data = data;
        }

        public RoleRepository() : base()
        {
        }

        public bool Add(Guid key, Role role)
        {
            RoleEndpoints.CreateRole(role);
            return true;
        }

        public bool Remove(Guid key)
        {
            RoleEndpoints.DeleteRole(key);
            return true;
        }

        public Role? Get(Guid key)
        {
            return RoleEndpoints.ReadRole().Find((role) => role.Id == key);
        }

        public IEnumerable<Role> GetAll()
        {
            return RoleEndpoints.ReadRole();
        }

        public bool Contains(Guid key)
        {
            return RoleEndpoints.ReadRole().Exists((role) => role.Id == key);
        }

        public bool Update(Guid key, Role value)
        {
            RoleEndpoints.UpdateRoleName(key, value.Name);
            RoleEndpoints.UpdateRolePermissions(key, value.Permissions);
            return true;
        }
    }
}