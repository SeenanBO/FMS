namespace FileManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addeddepartmentcolumninfilestable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Files", "Department", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Files", "Department");
        }
    }
}
