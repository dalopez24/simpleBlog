namespace simpleBlog.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TagPost : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "TagId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "TagId");
        }
    }
}
