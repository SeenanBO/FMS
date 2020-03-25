namespace FileManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedcolumndescriptioninfilestable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Files", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Files", "Description");
        }
    }
}
