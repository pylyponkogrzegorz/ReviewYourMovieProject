using System;
using System.Collections.Generic;

namespace ReviewYourMovie.Server.Models
{
    public partial class UserPrivilege
    {
        public UserPrivilege()
        {
            RoleToPrivileges = new HashSet<RoleToPrivilege>();
        }

        public int UserPrivilegeId { get; set; }
        public string PrivilegeName { get; set; } = null!;

        public virtual ICollection<RoleToPrivilege> RoleToPrivileges { get; set; }
    }
}
