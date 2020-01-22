namespace FYP.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testingDatabase : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Practitioners", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Practitioners", "Status", c => c.Int(nullable: false));
        }
    }
}
