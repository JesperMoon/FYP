namespace FYP.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPatient : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        ContactNumber1 = c.String(),
                        ContactNumber2 = c.String(nullable: false),
                        ContactNumber3 = c.String(nullable: false),
                        HomeAddress = c.String(),
                        PostalCode = c.Int(nullable: false),
                        State = c.String(),
                        BloodType = c.String(maxLength: 10),
                        UserName = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Gender = c.String(),
                        Religion = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        EmailAddress = c.String(),
                        Password = c.String(),
                        Salt = c.Binary(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Patients");
        }
    }
}
