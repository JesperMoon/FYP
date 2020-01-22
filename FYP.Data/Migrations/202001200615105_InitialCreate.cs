namespace FYP.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Practitioners",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Company = c.String(nullable: false),
                        OfficePhoneNumber = c.String(),
                        PostalCode = c.Int(nullable: false),
                        State = c.String(),
                        Role = c.String(),
                        Specialist = c.String(),
                        Qualification = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Gender = c.String(),
                        Religion = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        EmailAddress = c.String(),
                        Password = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Practitioners");
        }
    }
}
