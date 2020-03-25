namespace FileManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedatableandalteredfilesimages : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.File_Images", "FileId", "dbo.Files");
            DropForeignKey("dbo.File_Images", "FileImages_Id", "dbo.FilesImages");
            DropIndex("dbo.File_Images", new[] { "FileId" });
            DropIndex("dbo.File_Images", new[] { "FileImages_Id" });
            DropTable("dbo.File_Images");
            
            AddColumn("dbo.FilesImages", "FileId", c => c.Int(nullable: false));
   
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.File_Images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileId = c.Int(nullable: false),
                        FileImageId = c.Int(nullable: false),
                        FileImages_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.FilesImagesFiles", "Files_Id", "dbo.Files");
            DropForeignKey("dbo.FilesImagesFiles", "FilesImages_Id", "dbo.FilesImages");
            DropIndex("dbo.FilesImagesFiles", new[] { "Files_Id" });
            DropIndex("dbo.FilesImagesFiles", new[] { "FilesImages_Id" });
            DropColumn("dbo.FilesImages", "FileId");
            DropTable("dbo.FilesImagesFiles");
            CreateIndex("dbo.File_Images", "FileImages_Id");
            CreateIndex("dbo.File_Images", "FileId");
            AddForeignKey("dbo.File_Images", "FileImages_Id", "dbo.FilesImages", "Id");
            AddForeignKey("dbo.File_Images", "FileId", "dbo.Files", "Id", cascadeDelete: true);
        }
    }
}
