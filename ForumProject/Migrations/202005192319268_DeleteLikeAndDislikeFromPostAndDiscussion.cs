namespace ForumProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteLikeAndDislikeFromPostAndDiscussion : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Discussions", "Likes");
            DropColumn("dbo.Discussions", "Dislikes");
            DropColumn("dbo.Posts", "Likes");
            DropColumn("dbo.Posts", "Dislikes");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "Dislikes", c => c.Int(nullable: false));
            AddColumn("dbo.Posts", "Likes", c => c.Int(nullable: false));
            AddColumn("dbo.Discussions", "Dislikes", c => c.Int(nullable: false));
            AddColumn("dbo.Discussions", "Likes", c => c.Int(nullable: false));
        }
    }
}
