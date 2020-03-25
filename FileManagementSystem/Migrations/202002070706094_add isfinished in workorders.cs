namespace FileManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addisfinishedinworkorders : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorkOrders", "IsFinished", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.WorkOrders", "IsFinished");
        }
    }
}
