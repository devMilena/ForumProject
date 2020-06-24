namespace ForumProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CategoryData : DbMigration
    {
        public override void Up()
        {

            Sql("INSERT INTO Categories (Name,Description,DiscussionsCount) VALUES ('Programing', 'General programming topics',0)");
            Sql("INSERT INTO Categories (Name,Description,DiscussionsCount) VALUES ('C# programming language', 'Language theory and practice',0)");
            Sql("INSERT INTO Categories (Name,Description,DiscussionsCount) VALUES ('Web programming', 'Asp.Net framework',0)");
            Sql("INSERT INTO Categories (Name,Description,DiscussionsCount) VALUES ('Database programming', 'MS SQL and Entity framework',0)");
            Sql("INSERT INTO Categories (Name,Description,DiscussionsCount) VALUES ('General discussions', 'All other topics',0)");

        }

        public override void Down()
        {
        }
    }
}
