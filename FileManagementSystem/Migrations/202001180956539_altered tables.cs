namespace FileManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alteredtables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.File_Images",
                c => new
                    {
                        File_Images_Id = c.Int(nullable: false, identity: true),
                        File_Id = c.Int(),
                        FileImages_Id = c.Int(),
                    })
                .PrimaryKey(t => t.File_Images_Id)
                .ForeignKey("dbo.Files", t => t.File_Id)
                .ForeignKey("dbo.FilesImages", t => t.FileImages_Id)
                .Index(t => t.File_Id)
                .Index(t => t.FileImages_Id);
            
            CreateTable(
                "dbo.FilesImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImgPath = c.String(),
                        AddedBy = c.String(),
                        AddedTime = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                        ModifiedTime = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.Files", "ImgPath");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Files", "ImgPath", c => c.String());
            DropForeignKey("dbo.File_Images", "FileImages_Id", "dbo.FilesImages");
            DropForeignKey("dbo.File_Images", "File_Id", "dbo.Files");
            DropIndex("dbo.File_Images", new[] { "FileImages_Id" });
            DropIndex("dbo.File_Images", new[] { "File_Id" });
            DropTable("dbo.FilesImages");
            DropTable("dbo.File_Images");
        }
    }
}
