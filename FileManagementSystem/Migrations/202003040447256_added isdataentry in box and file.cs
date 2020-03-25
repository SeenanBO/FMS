namespace FileManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedisdataentryinboxandfile : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Boxes", "IsDataEntry", c => c.Boolean(nullable: false));
            AddColumn("dbo.Files", "IsDataEntry", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Files", "IsDataEntry");
            DropColumn("dbo.Boxes", "IsDataEntry");
        }
    }
}
