namespace OverflowVictor.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AccountLastName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "LastName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Accounts", "LastName");
        }
    }
}
