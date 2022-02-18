using System;
using System.Collections.Generic;

namespace ReviewYourMovie.Server.Models
{
    public partial class RoleChangeLog
    {
        public int RoleChangeLogId { get; set; }
        public DateTime EventTime { get; set; }
        public int UserId { get; set; }
        public int ChangeRoleFrom { get; set; }
        public int ChangeRoleTo { get; set; }
        public int SetByUser { get; set; }

        public virtual UserRole ChangeRoleFromNavigation { get; set; } = null!;
        public virtual UserRole ChangeRoleToNavigation { get; set; } = null!;
        public virtual User SetByUserNavigation { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
