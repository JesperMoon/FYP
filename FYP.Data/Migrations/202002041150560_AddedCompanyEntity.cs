namespace FYP.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCompanyEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        CompanyName = c.String(),
                        CompanyPhoneNumber = c.String(),
                        CompanyEmailAddress = c.String(),
                        CompanyAddressLine1 = c.String(),
                        CompanyAddressLine2 = c.String(),
                        CompanyAddressLine3 = c.String(),
                        PostalCode = c.Int(nullable: false),
                        City = c.String(),
                        State = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.Practitioners", "Company", c => c.Guid(nullable: false));
            DropColumn("dbo.Practitioners", "PostalCode");
            DropColumn("dbo.Practitioners", "State");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Practitioners", "State", c => c.String());
            AddColumn("dbo.Practitioners", "PostalCode", c => c.Int(nullable: false));
            AlterColumn("dbo.Practitioners", "Company", c => c.String(nullable: false));
            DropTable("dbo.Companies");
        }
    }
}
