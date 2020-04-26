namespace ForumProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        DicussionCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Discussions",
                c => new
                    {
                        DiscussionId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        Likes = c.Int(nullable: false),
                        Dislikes = c.Int(nullable: false),
                        PostCount = c.Int(nullable: false),
                        ViewCount = c.Int(nullable: false),
                        Description = c.String(nullable: false),
                        UserId = c.String(maxLength: 128),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DiscussionId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        PostId = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        Likes = c.Int(nullable: false),
                        Dislikes = c.Int(nullable: false),
                        DiscussionId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.PostId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.Discussions", t => t.DiscussionId, cascadeDelete: true)
                .Index(t => t.DiscussionId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "DiscussionId", "dbo.Discussions");
            DropForeignKey("dbo.Posts", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Discussions", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Discussions", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Posts", new[] { "UserId" });
            DropIndex("dbo.Posts", new[] { "DiscussionId" });
            DropIndex("dbo.Discussions", new[] { "CategoryId" });
            DropIndex("dbo.Discussions", new[] { "UserId" });
            DropTable("dbo.Posts");
            DropTable("dbo.Discussions");
            DropTable("dbo.Categories");
        }
    }
}
