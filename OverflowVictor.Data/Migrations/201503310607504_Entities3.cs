namespace OverflowVictor.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Entities3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Answers", "Title");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Answers", "Title", c => c.String());
        }
    }
}
