namespace CarIn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedWebServiceLoggerModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
            "dbo.WebServiceLogger",
            c => new
            {
                ID = c.Int(nullable: false, identity: true),
                DateTime = c.DateTime(),
                ClassName = c.String(),
                StatusCode = c.String(),
                StatusMessag = c.String()
            })
            .PrimaryKey(t => t.ID);
        }
        
        public override void Down()
        {
            DropTable("dbo.WebServiceLogger");

        }
    }
}
