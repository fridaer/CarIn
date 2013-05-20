namespace CarIn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        ShowWeather = c.Boolean(nullable: false),
                        ShowAccidents = c.Boolean(nullable: false),
                        ShowTraffic = c.Boolean(nullable: false),
                        ShowPublicTransport = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TrafficIncidents",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IncidentId = c.String(),
                        PointLat = c.String(),
                        PointLong = c.String(),
                        ToPointLat = c.String(),
                        ToPointLong = c.String(),
                        Start = c.String(),
                        End = c.String(),
                        LastModified = c.String(),
                        Description = c.String(),
                        Lane = c.String(),
                        Severity = c.String(),
                        IncidentType = c.String(),
                        RoadClosed = c.String(),
                        Verified = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.WheatherPeriods",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PeriodNumber = c.String(),
                        SymbolName = c.String(),
                        WindCode = c.String(),
                        WindSpeedMps = c.String(),
                        TemperatureCelsius = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.VasttrafikIncidents",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Line = c.String(),
                        DateFrom = c.String(),
                        DateTo = c.String(),
                        Priority = c.String(),
                        TrafficChangesCoords = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.VasttrafikIncidents");
            DropTable("dbo.WheatherPeriods");
            DropTable("dbo.TrafficIncidents");
            DropTable("dbo.Users");
        }
    }
}
