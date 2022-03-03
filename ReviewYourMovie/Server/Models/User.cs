using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ReviewYourMovie.Server.Models
{
    public partial class User
    {
        public User()
        {
            MovieReviews = new HashSet<MovieReview>();
            RoleChangeLogSetByUserNavigations = new HashSet<RoleChangeLog>();
            RoleChangeLogUsers = new HashSet<RoleChangeLog>();
            UserActivityLogs = new HashSet<UserActivityLog>();
            VoteReviews = new HashSet<VoteReview>();
        }

        [Key]
        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string EmailAddress { get; set; } = null!;
        public bool RegisterComplete { get; set; }
        public DateTime LastLogonTime { get; set; }
        public decimal? UserScore { get; set; }
        public int UserRoleId { get; set; }
        public DateTime RegisterTime { get; set; }
        public string? Token { get; set; }

        public virtual UserRole UserRole { get; set; } = null!;
        public virtual ICollection<MovieReview> MovieReviews { get; set; }
        public virtual ICollection<RoleChangeLog> RoleChangeLogSetByUserNavigations { get; set; }
        public virtual ICollection<RoleChangeLog> RoleChangeLogUsers { get; set; }
        public virtual ICollection<UserActivityLog> UserActivityLogs { get; set; }
        public virtual ICollection<VoteReview> VoteReviews { get; set; }
    }
}
