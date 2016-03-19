namespace simpleBlog.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OMG : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TagPosts", "Tag_TagId", "dbo.Tags");
            DropForeignKey("dbo.TagPosts", "Post_PostId", "dbo.Posts");
            DropIndex("dbo.TagPosts", new[] { "Tag_TagId" });
            DropIndex("dbo.TagPosts", new[] { "Post_PostId" });
            AddColumn("dbo.Tags", "Post_PostId", c => c.Int());
            CreateIndex("dbo.Tags", "Post_PostId");
            AddForeignKey("dbo.Tags", "Post_PostId", "dbo.Posts", "PostId");
            DropTable("dbo.TagPosts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TagPosts",
                c => new
                    {
                        Tag_TagId = c.Int(nullable: false),
                        Post_PostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_TagId, t.Post_PostId });
            
            DropForeignKey("dbo.Tags", "Post_PostId", "dbo.Posts");
            DropIndex("dbo.Tags", new[] { "Post_PostId" });
            DropColumn("dbo.Tags", "Post_PostId");
            CreateIndex("dbo.TagPosts", "Post_PostId");
            CreateIndex("dbo.TagPosts", "Tag_TagId");
            AddForeignKey("dbo.TagPosts", "Post_PostId", "dbo.Posts", "PostId", cascadeDelete: true);
            AddForeignKey("dbo.TagPosts", "Tag_TagId", "dbo.Tags", "TagId", cascadeDelete: true);
        }
    }
}
