namespace FileManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addediscompletedcolumninworkorders : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorkOrders", "IsCompleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.WorkOrders", "WorkOrderNumber", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.WorkOrders", "IsCompleted");
            DropColumn("dbo.WorkOrders", "WorkOrderNumber");
        }
    }
}
