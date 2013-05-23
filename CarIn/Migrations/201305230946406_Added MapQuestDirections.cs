namespace CarIn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedMapQuestDirections : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MapQuestDirections",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        shapePoints = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MapQuestDirections");
        }
    }
}
