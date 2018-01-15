namespace DataStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "usr.SysRoles",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 50, unicode: false),
                    Level = c.Int(nullable: false),
                    Status = c.String(nullable: false, maxLength: 1, unicode: false),
                    DateCreated = c.DateTime(nullable: false),
                    LastUpdated = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "usr.UserRoles",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    RoleId = c.Int(nullable: false),
                    UserId = c.Int(nullable: false),
                    Status = c.String(nullable: false, maxLength: 1, unicode: false),
                    DateCreated = c.DateTime(nullable: false),
                    LastUpdated = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("usr.UserDetails", t => t.UserId)
                .ForeignKey("usr.SysRoles", t => t.RoleId)
                .Index(t => t.RoleId)
                .Index(t => t.UserId);

            CreateTable(
                "usr.UserDetails",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Username = c.String(nullable: false, maxLength: 100, unicode: false),
                    Name = c.String(nullable: false, maxLength: 50, unicode: false),
                    Surname = c.String(nullable: false, maxLength: 50, unicode: false),
                    Email = c.String(nullable: false, maxLength: 50, unicode: false),
                    Password = c.String(nullable: false, unicode: false),
                    Status = c.String(nullable: false, maxLength: 1, unicode: false),
                    DateCreated = c.DateTime(nullable: false),
                    LastUpdated = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.ID);

        }
        
        public override void Down()
        {
            DropForeignKey("usr.UserRoles", "RoleId", "usr.SysRoles");
            DropForeignKey("usr.UserRoles", "UserId", "usr.UserDetails");
            DropIndex("usr.UserRoles", new[] { "UserId" });
            DropIndex("usr.UserRoles", new[] { "RoleId" });
            DropTable("usr.UserDetails");
            DropTable("usr.UserRoles");
            DropTable("usr.SysRoles");
        }
    }
}
