using EX.Model.DbLayer;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace EX.Model.Repositories.Administration
{
    public class RoleRepository
    {
        EContext context;

        public RoleRepository()
        {
            context = new EContext();
        }

        public Role AddOrUpdate(Role role)
        {
            if(context.Roles.Where(r => r.Name == role.Name).Count() == 0)
            {
                context.Roles.AddOrUpdate(role);
                context.SaveChanges();
                if (role.Id == 0)
                {
                    var roleId = context.Roles.Where(r => r.Name == role.Name).FirstOrDefault().Id;
                    new TabRepository(roleId);
                    new CommandRepository(roleId);
                }
            }
            return context.Roles.Where(r => r.Name == role.Name).FirstOrDefault();    
        }

        public void RemoveRole(Role role)
        {

            context.Roles.Remove(role);
            context.SaveChanges();
        }

        public IEnumerable<Role> GetAllRoles() { return context.Roles; }
    }
}
