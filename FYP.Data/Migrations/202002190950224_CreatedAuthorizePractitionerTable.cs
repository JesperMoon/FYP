namespace FYP.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatedAuthorizePractitionerTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuthorizePractitioners",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        PatientId = c.Guid(nullable: false),
                        PractitionerId = c.Guid(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AuthorizePractitioners");
        }
    }
}
