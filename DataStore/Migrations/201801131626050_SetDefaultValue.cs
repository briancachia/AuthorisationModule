namespace DataStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetDefaultValue : DbMigration
    {
        public override void Up()
        {
            //AddColumn("usr.UserDetails", "Status", s => s.String(defaultValueSql:"A"));
        }
        
        public override void Down()
        {
            DropColumn("usr.UserDetails", "Status");
        }
    }
}
