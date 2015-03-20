namespace OverflowVictor.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AccountConfirmation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Answers", "AccountId", "dbo.Accounts");
            DropForeignKey("dbo.Questions", "Account_Id", "dbo.Accounts");
            DropIndex("dbo.Answers", new[] { "AccountId" });
            DropIndex("dbo.Questions", new[] { "Account_Id" });
            AddColumn("dbo.Accounts", "Activated", c => c.Boolean(nullable: false));
            DropColumn("dbo.Questions", "Account_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Questions", "Account_Id", c => c.Guid());
            DropColumn("dbo.Accounts", "Activated");
            CreateIndex("dbo.Questions", "Account_Id");
            CreateIndex("dbo.Answers", "AccountId");
            AddForeignKey("dbo.Questions", "Account_Id", "dbo.Accounts", "Id");
            AddForeignKey("dbo.Answers", "AccountId", "dbo.Accounts", "Id", cascadeDelete: true);
        }
    }
}
