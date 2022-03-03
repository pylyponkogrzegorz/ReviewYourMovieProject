using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ReviewYourMovie.Server.Models;

namespace ReviewYourMovie.Server.Context
{
    public partial class UserContext : DbContext
    {
        public UserContext()
        {
        }

        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ActivityType> ActivityTypes { get; set; } = null!;
        public virtual DbSet<MovieReview> MovieReviews { get; set; } = null!;
        public virtual DbSet<RoleChangeLog> RoleChangeLogs { get; set; } = null!;
        public virtual DbSet<RoleToPrivilege> RoleToPrivileges { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserActivityLog> UserActivityLogs { get; set; } = null!;
        public virtual DbSet<UserPrivilege> UserPrivileges { get; set; } = null!;
        public virtual DbSet<UserRole> UserRoles { get; set; } = null!;
        public virtual DbSet<VoteReview> VoteReviews { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=LAPTOP-ODHDV0AR;Database=UsersDb;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActivityType>(entity =>
            {
                entity.ToTable("activity_type");

                entity.Property(e => e.ActivityTypeId)
                    .ValueGeneratedNever()
                    .HasColumnName("activity_type_id");

                entity.Property(e => e.ActivityTypeName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("activity_type_name");
            });

            modelBuilder.Entity<MovieReview>(entity =>
            {
                entity.HasKey(e => e.ReviewId);

                entity.ToTable("movie_review");

                entity.Property(e => e.ReviewId)
                    .ValueGeneratedNever()
                    .HasColumnName("review_id");

                entity.Property(e => e.MovieId).HasColumnName("movie_id");

                entity.Property(e => e.ReviewDatetime)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("review_datetime");

                entity.Property(e => e.ReviewDescription)
                    .HasColumnType("text")
                    .HasColumnName("review_description");

                entity.Property(e => e.ReviewRating)
                    .HasColumnType("decimal(5, 3)")
                    .HasColumnName("review_rating");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.UserScore)
                    .HasColumnType("decimal(5, 2)")
                    .HasColumnName("user_score");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.MovieReviews)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_movie_review_users");
            });

            modelBuilder.Entity<RoleChangeLog>(entity =>
            {
                entity.ToTable("role_change_log");

                entity.Property(e => e.RoleChangeLogId)
                    .ValueGeneratedNever()
                    .HasColumnName("role_change_log_id");

                entity.Property(e => e.ChangeRoleFrom).HasColumnName("change_role_from");

                entity.Property(e => e.ChangeRoleTo).HasColumnName("change_role_to");

                entity.Property(e => e.EventTime)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("event_time");

                entity.Property(e => e.SetByUser).HasColumnName("set_by_user");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.ChangeRoleFromNavigation)
                    .WithMany(p => p.RoleChangeLogChangeRoleFromNavigations)
                    .HasForeignKey(d => d.ChangeRoleFrom)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_role_change_log_user_roles");

                entity.HasOne(d => d.ChangeRoleToNavigation)
                    .WithMany(p => p.RoleChangeLogChangeRoleToNavigations)
                    .HasForeignKey(d => d.ChangeRoleTo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_role_change_log_user_roles1");

                entity.HasOne(d => d.SetByUserNavigation)
                    .WithMany(p => p.RoleChangeLogSetByUserNavigations)
                    .HasForeignKey(d => d.SetByUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_role_change_log_users1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RoleChangeLogUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_role_change_log_users");
            });

            modelBuilder.Entity<RoleToPrivilege>(entity =>
            {
                entity.ToTable("role_to_privileges");

                entity.Property(e => e.RoleToPrivilegeId)
                    .ValueGeneratedNever()
                    .HasColumnName("role_to_privilege_id");

                entity.Property(e => e.UserPrivilegeId).HasColumnName("user_privilege_id");

                entity.Property(e => e.UserRoleId).HasColumnName("user_role_id");

                entity.HasOne(d => d.UserPrivilege)
                    .WithMany(p => p.RoleToPrivileges)
                    .HasForeignKey(d => d.UserPrivilegeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_role_to_privileges_user_privileges");

                entity.HasOne(d => d.UserRole)
                    .WithMany(p => p.RoleToPrivileges)
                    .HasForeignKey(d => d.UserRoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_role_to_privileges_user_roles");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                //entity.Property(e => e.UserId)
                //    .ValueGeneratedNever()
                //    .UseIdentityColumn(1,1)
                //    .HasColumnName("user_id");
                
                entity.Property(e => e.UserId)
                    .HasColumnName("user_id");

                entity.Property(e => e.EmailAddress)
                    .HasMaxLength(50)
                    .HasColumnName("email_address");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastLogonTime)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("last_logon_time");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .HasColumnName("last_name");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .HasColumnName("password");

                entity.Property(e => e.RegisterComplete).HasColumnName("register_complete");

                entity.Property(e => e.RegisterTime)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("register_time");

                entity.Property(e => e.Token)
                    .HasMaxLength(56)
                    .IsUnicode(false)
                    .HasColumnName("token");

                entity.Property(e => e.UserRoleId).HasColumnName("user_role_id");

                entity.Property(e => e.UserScore)
                    .HasColumnType("decimal(5, 2)")
                    .HasColumnName("user_score");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .HasColumnName("username");

                entity.HasOne(d => d.UserRole)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserRoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_users_user_roles");
            });

            modelBuilder.Entity<UserActivityLog>(entity =>
            {
                entity.HasKey(e => e.ActivityId);

                entity.ToTable("user_activity_log");

                entity.Property(e => e.ActivityId)
                    .ValueGeneratedNever()
                    .HasColumnName("activity_id");

                entity.Property(e => e.ActivityDatetime)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("activity_datetime");

                entity.Property(e => e.ActivityDescription)
                    .HasColumnType("text")
                    .HasColumnName("activity_description");

                entity.Property(e => e.ActivityTypeId).HasColumnName("activity_type_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.ActivityType)
                    .WithMany(p => p.UserActivityLogs)
                    .HasForeignKey(d => d.ActivityTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_user_activity_log_activity_type");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserActivityLogs)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_user_activity_log_users");
            });

            modelBuilder.Entity<UserPrivilege>(entity =>
            {
                entity.ToTable("user_privileges");

                entity.Property(e => e.UserPrivilegeId)
                    .ValueGeneratedNever()
                    .HasColumnName("user_privilege_id");

                entity.Property(e => e.PrivilegeName)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("privilege_name");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("user_roles");

                entity.Property(e => e.UserRoleId)
                    .ValueGeneratedNever()
                    .HasColumnName("user_role_id");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("role_name");
            });

            modelBuilder.Entity<VoteReview>(entity =>
            {
                entity.ToTable("vote_review");

                entity.Property(e => e.VoteReviewId)
                    .ValueGeneratedNever()
                    .HasColumnName("vote_review_id");

                entity.Property(e => e.ReviewId).HasColumnName("review_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.UserScore)
                    .HasColumnType("decimal(5, 2)")
                    .HasColumnName("user_score");

                entity.Property(e => e.VoteDatetime)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("vote_datetime");

                entity.Property(e => e.VoteReviewDescription)
                    .HasColumnType("text")
                    .HasColumnName("vote_review_description");

                entity.Property(e => e.VoteReviewRating)
                    .HasColumnType("decimal(5, 3)")
                    .HasColumnName("vote_review_rating");

                entity.HasOne(d => d.Review)
                    .WithMany(p => p.VoteReviews)
                    .HasForeignKey(d => d.ReviewId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_vote_review_movie_review");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.VoteReviews)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_vote_review_users");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
