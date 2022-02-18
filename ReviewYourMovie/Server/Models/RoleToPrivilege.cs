using System;
using System.Collections.Generic;

namespace ReviewYourMovie.Server.Models
{
    public partial class RoleToPrivilege
    {
        public int RoleToPrivilegeId { get; set; }
        public int UserRoleId { get; set; }
        public int UserPrivilegeId { get; set; }

        public virtual UserPrivilege UserPrivilege { get; set; } = null!;
        public virtual UserRole UserRole { get; set; } = null!;
    }
}
