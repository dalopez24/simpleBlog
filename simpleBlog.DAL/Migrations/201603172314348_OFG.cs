namespace simpleBlog.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OFG : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tags", "Post_PostId", "dbo.Posts");
            DropIndex("dbo.Tags", new[] { "Post_PostId" });
            CreateTable(
                "dbo.TagPosts",
                c => new
                    {
                        Tag_TagId = c.Int(nullable: false),
                        Post_PostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_TagId, t.Post_PostId })
                .ForeignKey("dbo.Tags", t => t.Tag_TagId, cascadeDelete: true)
                .ForeignKey("dbo.Posts", t => t.Post_PostId, cascadeDelete: true)
                .Index(t => t.Tag_TagId)
                .Index(t => t.Post_PostId);
            
            DropColumn("dbo.Tags", "Post_PostId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tags", "Post_PostId", c => c.Int());
            DropForeignKey("dbo.TagPosts", "Post_PostId", "dbo.Posts");
            DropForeignKey("dbo.TagPosts", "Tag_TagId", "dbo.Tags");
            DropIndex("dbo.TagPosts", new[] { "Post_PostId" });
            DropIndex("dbo.TagPosts", new[] { "Tag_TagId" });
            DropTable("dbo.TagPosts");
            CreateIndex("dbo.Tags", "Post_PostId");
            AddForeignKey("dbo.Tags", "Post_PostId", "dbo.Posts", "PostId");
        }
    }
}
