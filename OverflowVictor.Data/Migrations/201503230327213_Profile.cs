namespace OverflowVictor.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Profile : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "RegisterDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Accounts", "Views", c => c.Int(nullable: false));
            AddColumn("dbo.Accounts", "LastSeen", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Accounts", "LastSeen");
            DropColumn("dbo.Accounts", "Views");
            DropColumn("dbo.Accounts", "RegisterDate");
        }
    }
}
