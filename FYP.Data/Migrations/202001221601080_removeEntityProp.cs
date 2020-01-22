namespace FYP.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeEntityProp : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Practitioners", "CreatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Practitioners", "ModifiedOn", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Practitioners", "ModifiedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Practitioners", "CreatedOn", c => c.DateTime(nullable: false));
        }
    }
}
