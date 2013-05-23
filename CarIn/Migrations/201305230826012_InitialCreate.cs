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
                "dbo.TollLocations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TrafikVID = c.Int(nullable: false),
                        Name = c.String(),
                        Direction = c.String(),
                        LaneNumbers = c.Int(nullable: false),
                        PointLong = c.String(),
                        PointLat = c.String(),
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
            
            CreateTable(
                "dbo.WebServiceLoggs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LogginTime = c.DateTime(nullable: false),
                        ClassName = c.String(),
                        StatusCode = c.String(),
                        StatusMessag = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            
            //Sql("TRUNCATE TABLE dbo.Users", true );
            //Sql("TRUNCATE TABLE dbo.TollLocations");
            //Sql("TRUNCATE TABLE dbo.TrafficIncidents");
            //Sql("TRUNCATE TABLE dbo.WheatherPeriods");
            //Sql("TRUNCATE TABLE dbo.VasttrafikIncidents");
            //Sql("TRUNCATE TABLE dbo.WebServiceLoggs");

        }
        
        public override void Down()
        {
            DropTable("dbo.WebServiceLoggs");
            DropTable("dbo.VasttrafikIncidents");
            DropTable("dbo.WheatherPeriods");
            DropTable("dbo.TrafficIncidents");
            DropTable("dbo.TollLocations");
            DropTable("dbo.Users");
        }
    }
}
