using System.Collections.Generic;

namespace EX.Model.DbLayer
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<UserInRole> UserInRoles { get; set; }
    }
}
