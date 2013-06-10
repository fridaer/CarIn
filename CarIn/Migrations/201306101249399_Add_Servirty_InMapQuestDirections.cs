namespace CarIn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Servirty_InMapQuestDirections : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MapQuestDirections", "Severity", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MapQuestDirections", "Severity");
        }
    }
}
