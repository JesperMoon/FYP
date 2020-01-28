namespace FYP.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SaltAndStatusString : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Practitioners", "Salt", c => c.Binary());
            AlterColumn("dbo.Practitioners", "Status", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Practitioners", "Status", c => c.Int(nullable: false));
            DropColumn("dbo.Practitioners", "Salt");
        }
    }
}
