namespace ForumProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateUsersPostLikesAndUsersPostDislikesTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserPostDislikes",
                c => new
                    {
                        UserPostDislikeId = c.Int(nullable: false, identity: true),
                        PostId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserPostDislikeId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.Posts", t => t.PostId, cascadeDelete: true)
                .Index(t => t.PostId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserPostLikes",
                c => new
                    {
                        UserPostLikeId = c.Int(nullable: false, identity: true),
                        PostId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserPostLikeId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.Posts", t => t.PostId, cascadeDelete: true)
                .Index(t => t.PostId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserPostLikes", "PostId", "dbo.Posts");
            DropForeignKey("dbo.UserPostLikes", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserPostDislikes", "PostId", "dbo.Posts");
            DropForeignKey("dbo.UserPostDislikes", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.UserPostLikes", new[] { "UserId" });
            DropIndex("dbo.UserPostLikes", new[] { "PostId" });
            DropIndex("dbo.UserPostDislikes", new[] { "UserId" });
            DropIndex("dbo.UserPostDislikes", new[] { "PostId" });
            DropTable("dbo.UserPostLikes");
            DropTable("dbo.UserPostDislikes");
        }
    }
}
