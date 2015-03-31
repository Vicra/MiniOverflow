namespace OverflowVictor.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Entities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        Description = c.String(),
                        Answer_Id = c.Guid(),
                        Owner_Id = c.Guid(),
                        Question_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Answers", t => t.Answer_Id)
                .ForeignKey("dbo.Accounts", t => t.Owner_Id)
                .ForeignKey("dbo.Questions", t => t.Question_Id)
                .Index(t => t.Answer_Id)
                .Index(t => t.Owner_Id)
                .Index(t => t.Question_Id);
            
            CreateTable(
                "dbo.Votes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Answer_Id = c.Guid(),
                        Question_Id = c.Guid(),
                        Voter_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Answers", t => t.Answer_Id)
                .ForeignKey("dbo.Questions", t => t.Question_Id)
                .ForeignKey("dbo.Accounts", t => t.Voter_Id)
                .Index(t => t.Answer_Id)
                .Index(t => t.Question_Id)
                .Index(t => t.Voter_Id);
            
            AddColumn("dbo.Questions", "Account_Id", c => c.Guid());
            CreateIndex("dbo.Answers", "AccountId");
            CreateIndex("dbo.Questions", "Account_Id");
            AddForeignKey("dbo.Answers", "AccountId", "dbo.Accounts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Questions", "Account_Id", "dbo.Accounts", "Id");
            DropTable("dbo.AnswerComments");
            DropTable("dbo.QuestionComments");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.QuestionComments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AccountId = c.Guid(nullable: false),
                        QuestionId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AnswerComments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AccountId = c.Guid(nullable: false),
                        AnswerrId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.Questions", "Account_Id", "dbo.Accounts");
            DropForeignKey("dbo.Answers", "AccountId", "dbo.Accounts");
            DropForeignKey("dbo.Votes", "Voter_Id", "dbo.Accounts");
            DropForeignKey("dbo.Votes", "Question_Id", "dbo.Questions");
            DropForeignKey("dbo.Votes", "Answer_Id", "dbo.Answers");
            DropForeignKey("dbo.Comments", "Question_Id", "dbo.Questions");
            DropForeignKey("dbo.Comments", "Owner_Id", "dbo.Accounts");
            DropForeignKey("dbo.Comments", "Answer_Id", "dbo.Answers");
            DropIndex("dbo.Votes", new[] { "Voter_Id" });
            DropIndex("dbo.Votes", new[] { "Question_Id" });
            DropIndex("dbo.Votes", new[] { "Answer_Id" });
            DropIndex("dbo.Questions", new[] { "Account_Id" });
            DropIndex("dbo.Comments", new[] { "Question_Id" });
            DropIndex("dbo.Comments", new[] { "Owner_Id" });
            DropIndex("dbo.Comments", new[] { "Answer_Id" });
            DropIndex("dbo.Answers", new[] { "AccountId" });
            DropColumn("dbo.Questions", "Account_Id");
            DropTable("dbo.Votes");
            DropTable("dbo.Comments");
        }
    }
}
