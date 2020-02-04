namespace FYP.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PatientAddressAndCity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Patients", "HomeAddress1", c => c.String());
            AddColumn("dbo.Patients", "HomeAddress2", c => c.String());
            AddColumn("dbo.Patients", "HomeAddress3", c => c.String());
            AddColumn("dbo.Patients", "City", c => c.String());
            DropColumn("dbo.Patients", "HomeAddress");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Patients", "HomeAddress", c => c.String());
            DropColumn("dbo.Patients", "City");
            DropColumn("dbo.Patients", "HomeAddress3");
            DropColumn("dbo.Patients", "HomeAddress2");
            DropColumn("dbo.Patients", "HomeAddress1");
        }
    }
}
