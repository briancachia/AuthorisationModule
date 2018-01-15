namespace DataStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetDefaultValueUserDetails2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("usr.UserDetails", "Status");
            AddColumn("usr.UserDetails", "Status", s=>s.String(nullable:false, defaultValue:"A", defaultValueSql:"A"));
        }
        
        public override void Down()
        {
        }
    }
}
