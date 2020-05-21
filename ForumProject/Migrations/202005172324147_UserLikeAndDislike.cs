namespace ForumProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserLikeAndDislike : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Like", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "Dislike", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Dislike");
            DropColumn("dbo.AspNetUsers", "Like");
        }
    }
}
