namespace OverflowVictor.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class column : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Answers", "Correct", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Answers", "Correct");
        }
    }
}
