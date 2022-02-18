using System;
using System.Collections.Generic;

namespace ReviewYourMovie.Server.Models
{
    public partial class UserRole
    {
        public UserRole()
        {
            RoleChangeLogChangeRoleFromNavigations = new HashSet<RoleChangeLog>();
            RoleChangeLogChangeRoleToNavigations = new HashSet<RoleChangeLog>();
            RoleToPrivileges = new HashSet<RoleToPrivilege>();
            Users = new HashSet<User>();
        }

        public int UserRoleId { get; set; }
        public string RoleName { get; set; } = null!;

        public virtual ICollection<RoleChangeLog> RoleChangeLogChangeRoleFromNavigations { get; set; }
        public virtual ICollection<RoleChangeLog> RoleChangeLogChangeRoleToNavigations { get; set; }
        public virtual ICollection<RoleToPrivilege> RoleToPrivileges { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
