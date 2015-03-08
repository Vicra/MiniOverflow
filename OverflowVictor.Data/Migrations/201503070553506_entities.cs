namespace OverflowVictor.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class entities : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "Account_Id", c => c.Guid());
            CreateIndex("dbo.Answers", "AccountId");
            CreateIndex("dbo.Questions", "Account_Id");
            AddForeignKey("dbo.Answers", "AccountId", "dbo.Accounts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Questions", "Account_Id", "dbo.Accounts", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Questions", "Account_Id", "dbo.Accounts");
            DropForeignKey("dbo.Answers", "AccountId", "dbo.Accounts");
            DropIndex("dbo.Questions", new[] { "Account_Id" });
            DropIndex("dbo.Answers", new[] { "AccountId" });
            DropColumn("dbo.Questions", "Account_Id");
        }
    }
}
