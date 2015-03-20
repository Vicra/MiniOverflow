namespace OverflowVictor.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        LastName = c.String(),
                        Password = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
                        Description = c.String(),
                        Votes = c.Int(nullable: false),
                        AccountId = c.Guid(nullable: false),
                        QuestionId = c.Guid(nullable: false),
                        Correct = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.AccountId, cascadeDelete: true)
                .ForeignKey("dbo.Questions", t => t.QuestionId, cascadeDelete: true)
                .Index(t => t.AccountId)
                .Index(t => t.QuestionId);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Votes = c.Int(nullable: false),
                        Title = c.String(),
                        Description = c.String(),
                        Owner = c.Guid(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        ModificationDate = c.DateTime(nullable: false),
                        HasCorrectAnswer = c.Boolean(nullable: false),
                        Account_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.Account_Id)
                .Index(t => t.Account_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Questions", "Account_Id", "dbo.Accounts");
            DropForeignKey("dbo.Answers", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.Answers", "AccountId", "dbo.Accounts");
            DropIndex("dbo.Questions", new[] { "Account_Id" });
            DropIndex("dbo.Answers", new[] { "QuestionId" });
            DropIndex("dbo.Answers", new[] { "AccountId" });
            DropTable("dbo.Questions");
            DropTable("dbo.Answers");
            DropTable("dbo.Accounts");
        }
    }
}
