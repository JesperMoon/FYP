namespace FYP.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatedAppointmentTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        AppointmentDateTime = c.DateTime(nullable: false),
                        PractitionerId = c.Guid(nullable: false),
                        PatientId = c.Guid(nullable: false),
                        Description = c.String(),
                        Remarks = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Appointments");
        }
    }
}
