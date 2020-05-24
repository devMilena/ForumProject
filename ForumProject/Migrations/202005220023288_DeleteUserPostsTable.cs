namespace ForumProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteUserPostsTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserPosts", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserPosts", "PostId", "dbo.Posts");
            DropIndex("dbo.UserPosts", new[] { "PostId" });
            DropIndex("dbo.UserPosts", new[] { "UserId" });
            DropTable("dbo.UserPosts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserPosts",
                c => new
                    {
                        UserPostId = c.Int(nullable: false, identity: true),
                        PostId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserPostId);
            
            CreateIndex("dbo.UserPosts", "UserId");
            CreateIndex("dbo.UserPosts", "PostId");
            AddForeignKey("dbo.UserPosts", "PostId", "dbo.Posts", "PostId", cascadeDelete: true);
            AddForeignKey("dbo.UserPosts", "UserId", "dbo.AspNetUsers", "Id");
        }
    }
}
