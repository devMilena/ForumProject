namespace ForumProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTablesforce : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "DiscussionsCount", c => c.Int(nullable: false));
            DropColumn("dbo.Categories", "DicussionCount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categories", "DicussionCount", c => c.Int(nullable: false));
            DropColumn("dbo.Categories", "DiscussionsCount");
        }
    }
}
