namespace ForumProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyThePostTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Posts", "Text", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Posts", "Text", c => c.String());
        }
    }
}
