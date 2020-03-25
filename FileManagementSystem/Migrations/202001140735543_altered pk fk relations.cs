namespace FileManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alteredpkfkrelations : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Boxes", "WorkOrderId");
            DropColumn("dbo.Files", "BoxId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Files", "BoxId", c => c.Int(nullable: false));
            AddColumn("dbo.Boxes", "WorkOrderId", c => c.Int(nullable: false));
        }
    }
}
