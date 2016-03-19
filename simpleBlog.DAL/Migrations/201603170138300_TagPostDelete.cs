namespace simpleBlog.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TagPostDelete : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Posts", "TagId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "TagId", c => c.Int(nullable: false));
        }
    }
}
