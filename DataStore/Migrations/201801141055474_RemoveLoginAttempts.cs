namespace DataStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveLoginAttempts : DbMigration
    {
        public override void Up()
        {
            AddColumn("usr.UserDetails", "LoginAttempts", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("usr.UserDetails", "LoginAttempts");
        }
    }
}
