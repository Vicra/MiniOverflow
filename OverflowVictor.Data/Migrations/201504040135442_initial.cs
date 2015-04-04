namespace OverflowVictor.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
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
                        Activated = c.Boolean(nullable: false),
                        RegisterDate = c.DateTime(nullable: false),
                        Views = c.Int(nullable: false),
                        LastSeen = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Description = c.String(),
                        Votes = c.Int(nullable: false),
                        AccountId = c.Guid(nullable: false),
                        QuestionId = c.Guid(nullable: false),
                        Correct = c.Boolean(nullable: false),
                        Views = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Questions", t => t.QuestionId, cascadeDelete: true)
                .Index(t => t.QuestionId);
            
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
                        Views = c.Int(nullable: false),
                        AnswerCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Votes", "Voter_Id", "dbo.Accounts");
            DropForeignKey("dbo.Votes", "Question_Id", "dbo.Questions");
            DropForeignKey("dbo.Votes", "Answer_Id", "dbo.Answers");
            DropForeignKey("dbo.Comments", "Question_Id", "dbo.Questions");
            DropForeignKey("dbo.Answers", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.Comments", "Owner_Id", "dbo.Accounts");
            DropForeignKey("dbo.Comments", "Answer_Id", "dbo.Answers");
            DropIndex("dbo.Votes", new[] { "Voter_Id" });
            DropIndex("dbo.Votes", new[] { "Question_Id" });
            DropIndex("dbo.Votes", new[] { "Answer_Id" });
            DropIndex("dbo.Comments", new[] { "Question_Id" });
            DropIndex("dbo.Comments", new[] { "Owner_Id" });
            DropIndex("dbo.Comments", new[] { "Answer_Id" });
            DropIndex("dbo.Answers", new[] { "QuestionId" });
            DropTable("dbo.Votes");
            DropTable("dbo.Questions");
            DropTable("dbo.Comments");
            DropTable("dbo.Answers");
            DropTable("dbo.Accounts");
        }
    }
}
