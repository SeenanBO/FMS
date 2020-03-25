namespace FileManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alteredtablerelatons : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Boxes", "WorkOrderId", c => c.Int(nullable: false));
            AddColumn("dbo.Boxes", "WorkOrders_Id", c => c.Int());
            AddColumn("dbo.Files", "BoxId", c => c.Int(nullable: false));
            AddColumn("dbo.Files", "Boxes_Id", c => c.Int());
            CreateIndex("dbo.Boxes", "WorkOrders_Id");
            CreateIndex("dbo.Files", "Boxes_Id");
            AddForeignKey("dbo.Files", "Boxes_Id", "dbo.Boxes", "Id");
            AddForeignKey("dbo.Boxes", "WorkOrders_Id", "dbo.WorkOrders", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Boxes", "WorkOrders_Id", "dbo.WorkOrders");
            DropForeignKey("dbo.Files", "Boxes_Id", "dbo.Boxes");
            DropIndex("dbo.Files", new[] { "Boxes_Id" });
            DropIndex("dbo.Boxes", new[] { "WorkOrders_Id" });
            DropColumn("dbo.Files", "Boxes_Id");
            DropColumn("dbo.Files", "BoxId");
            DropColumn("dbo.Boxes", "WorkOrders_Id");
            DropColumn("dbo.Boxes", "WorkOrderId");
        }
    }
}
