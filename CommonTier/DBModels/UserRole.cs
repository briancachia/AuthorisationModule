namespace CommonTier.DBModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("usr.UserRoles")]
    public partial class UserRole
    {
        public UserRole()
        {
            Status = "A";
            DateCreated = DateTime.Now;
            LastUpdated = DateTime.Now;
        }

        public int ID { get; set; }

        public int RoleId { get; set; }

        public int UserId { get; set; }

        [Required]
        [StringLength(1)]
        public string Status { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime LastUpdated { get; set; }

        public virtual SysRole SysRole { get; set; }

        public virtual UserDetail UserDetail { get; set; }
    }
}
