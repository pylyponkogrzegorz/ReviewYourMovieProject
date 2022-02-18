using System;
using System.Collections.Generic;

namespace ReviewYourMovie.Server.Models
{
    public partial class ActivityType
    {
        public ActivityType()
        {
            UserActivityLogs = new HashSet<UserActivityLog>();
        }

        public int ActivityTypeId { get; set; }
        public string ActivityTypeName { get; set; } = null!;

        public virtual ICollection<UserActivityLog> UserActivityLogs { get; set; }
    }
}
