namespace OverflowVictor.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Comments : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Answers", "Views", c => c.Int(nullable: false));
            AddColumn("dbo.Questions", "Views", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Questions", "Views");
            DropColumn("dbo.Answers", "Views");
        }
    }
}
