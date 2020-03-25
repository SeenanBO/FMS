namespace FileManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alteredboxandfiletable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Boxes", "WorkOrderId", c => c.Int(nullable: false));
            AddColumn("dbo.Files", "BoxId", c => c.Int(nullable: false));
            AddColumn("dbo.Boxes", "BoxBarcode", c => c.String(nullable: false));
            AddColumn("dbo.Files", "FileBarcode", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Files", "BoxId");
            DropColumn("dbo.Boxes", "WorkOrderId");
            DropColumn("dbo.Boxes", "BoxBarcode");
            DropColumn("dbo.Files", "FileBarcode");
        }
    }
}
