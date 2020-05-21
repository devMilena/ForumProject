namespace ForumProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateUsersDiscussionLikesAndUsersDiscussionDislikesTablesforce : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserDiscussionDislikes",
                c => new
                    {
                        UserDiscussionDislikeId = c.Int(nullable: false, identity: true),
                        DiscussionId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserDiscussionDislikeId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.Discussions", t => t.DiscussionId, cascadeDelete: true)
                .Index(t => t.DiscussionId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserDiscussionLikes",
                c => new
                    {
                        UserDiscussionLikeId = c.Int(nullable: false, identity: true),
                        DiscussionId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserDiscussionLikeId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.Discussions", t => t.DiscussionId, cascadeDelete: true)
                .Index(t => t.DiscussionId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserDiscussionLikes", "DiscussionId", "dbo.Discussions");
            DropForeignKey("dbo.UserDiscussionLikes", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserDiscussionDislikes", "DiscussionId", "dbo.Discussions");
            DropForeignKey("dbo.UserDiscussionDislikes", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.UserDiscussionLikes", new[] { "UserId" });
            DropIndex("dbo.UserDiscussionLikes", new[] { "DiscussionId" });
            DropIndex("dbo.UserDiscussionDislikes", new[] { "UserId" });
            DropIndex("dbo.UserDiscussionDislikes", new[] { "DiscussionId" });
            DropTable("dbo.UserDiscussionLikes");
            DropTable("dbo.UserDiscussionDislikes");
        }
    }
}
