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
                        Activated = c.Boolean(nullable: false),
                        RegisterDate = c.DateTime(nullable: false),
                        Views = c.Int(nullable: false),
                        LastSeen = c.DateTime(nullable: false),
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
                        Views = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Questions", t => t.QuestionId, cascadeDelete: true)
                .Index(t => t.QuestionId);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Answers", "QuestionId", "dbo.Questions");
            DropIndex("dbo.Answers", new[] { "QuestionId" });
            DropTable("dbo.Questions");
            DropTable("dbo.QuestionComments");
            DropTable("dbo.Answers");
            DropTable("dbo.AnswerComments");
            DropTable("dbo.Accounts");
        }
    }
}
