namespace FYP.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserNameColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Practitioners", "UserName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Practitioners", "UserName");
        }
    }
}
