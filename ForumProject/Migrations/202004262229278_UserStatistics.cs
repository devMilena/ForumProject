namespace ForumProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserStatistics : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "DiscussionsCount", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "PostsCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "PostsCount");
            DropColumn("dbo.AspNetUsers", "DiscussionsCount");
        }
    }
}
