namespace DataStore
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using CommonTier.DBModels;

    public partial class AuthenticationContext : DbContext
    {
        /// <summary>
        /// Main, parameterless, constructor for the model
        /// </summary>
        public AuthenticationContext()
            : base("name=AuthenticationContext")
        {
        }

        /// <summary>
        /// You can specify connection from config file here.
        /// </summary>
        /// <param name="connectionName">Connection name found in config file</param>
        public AuthenticationContext(string connectionName): base("name="+ connectionName) { }

        /// <summary>
        /// Table for System roles
        /// </summary>
        public virtual DbSet<SysRole> SysRoles { get; set; }

        /// <summary>
        /// Table for User details (ex: Name, Surname, Username, email etc.)
        /// </summary>
        public virtual DbSet<UserDetail> UserDetails { get; set; }

        /// <summary>
        /// Table for the user-role allocation
        /// </summary>
        public virtual DbSet<UserRole> UserRoles { get; set; }

        /// <summary>
        /// Run whilst creating the database model
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SysRole>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<SysRole>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<SysRole>()
                .HasMany(e => e.UserRoles)
                .WithRequired(e => e.SysRole)
                .HasForeignKey(e => e.RoleId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserDetail>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<UserDetail>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<UserDetail>()
                .Property(e => e.Surname)
                .IsUnicode(false);

            modelBuilder.Entity<UserDetail>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<UserDetail>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<UserDetail>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<UserDetail>()
                .HasMany(e => e.UserRoles)
                .WithRequired(e => e.UserDetail)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserRole>()
                .Property(e => e.Status)
                .IsUnicode(false);
        }
    }
}
