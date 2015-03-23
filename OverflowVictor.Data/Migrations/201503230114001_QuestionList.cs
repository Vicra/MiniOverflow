namespace OverflowVictor.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuestionList : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "AnswerCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Questions", "AnswerCount");
        }
    }
}
