namespace ForumProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteUserLikeAndDislike : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "Like");
            DropColumn("dbo.AspNetUsers", "Dislike");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Dislike", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "Like", c => c.Int(nullable: false));
        }
    }
}
