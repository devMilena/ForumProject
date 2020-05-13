namespace ForumProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteRequiredAnnotationFromthePostTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Posts", "Text", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Posts", "Text", c => c.String(nullable: false));
        }
    }
}
