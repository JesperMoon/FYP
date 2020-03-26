namespace FYP.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_fileRecord_table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PatientRecords",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        ContentType = c.String(),
                        FileContents = c.Binary(),
                        FileDownloadname = c.String(),
                        PatientId = c.Guid(nullable: false),
                        PractitionerId = c.Guid(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PatientRecords");
        }
    }
}
