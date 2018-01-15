namespace DataStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateLoginAttempts : DbMigration
    {
        public override void Up()
        {
            AddColumn("usr.UserDetails", "LoginAttempts", l => l.Byte(defaultValue: 0, nullable: false));
        }
        
        public override void Down()
        {
        }
    }
}
