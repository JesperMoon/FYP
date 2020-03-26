namespace FYP.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_RejectedReasons_Column : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "RejectedReasons", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Appointments", "RejectedReasons");
        }
    }
}
