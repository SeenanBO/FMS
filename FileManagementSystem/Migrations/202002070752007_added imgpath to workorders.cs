namespace FileManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedimgpathtoworkorders : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorkOrders", "ImgPath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.WorkOrders", "ImgPath");
        }
    }
}
