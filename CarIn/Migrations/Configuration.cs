using System.Collections.Generic;
using CarIn.BLL;
using CarIn.Models.Entities;

namespace CarIn.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CarIn.DAL.Context.CarInContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CarIn.DAL.Context.CarInContext context)
        {
            context.Database.ExecuteSqlCommand("TRUNCATE TABLE dbo.Users");
            context.Database.ExecuteSqlCommand("TRUNCATE TABLE dbo.TollLocations");
            context.Database.ExecuteSqlCommand("TRUNCATE TABLE dbo.TrafficIncidents");
            context.Database.ExecuteSqlCommand("TRUNCATE TABLE dbo.WheatherPeriods");
            context.Database.ExecuteSqlCommand("TRUNCATE TABLE dbo.VasttrafikIncidents");
            context.Database.ExecuteSqlCommand("TRUNCATE TABLE dbo.WebServiceLoggs");
            context.Database.ExecuteSqlCommand("TRUNCATE TABLE dbo.MapQuestDirections");

            var passHelper = new PasswordHelper();
            var Users = new List<User>
            {

                new User { ID = 1, Username = "Tobiasen" , Password = passHelper.HashPassword("delösersig", passHelper.GenerateSalt()), ShowAccidents = false, ShowPublicTransport = false, ShowTraffic = false, ShowWeather = true},
                new User { ID = 2, Username = "Björn" , Password = passHelper.HashPassword("delösersig", passHelper.GenerateSalt()), ShowAccidents = true, ShowPublicTransport = false, ShowTraffic = false, ShowWeather = true},
                new User { ID = 3, Username = "Frida" , Password = passHelper.HashPassword("delösersig", passHelper.GenerateSalt()), ShowAccidents = true, ShowPublicTransport = false, ShowTraffic = false, ShowWeather = false},
                new User { ID = 4, Username = "Fredrik" ,  Password = passHelper.HashPassword("delösersig", passHelper.GenerateSalt()), ShowAccidents = false, ShowPublicTransport = true, ShowTraffic = false, ShowWeather = false},
                new User { ID = 5, Username = "Urban" ,  Password = passHelper.HashPassword("delösersig", passHelper.GenerateSalt()), ShowAccidents = false, ShowPublicTransport = false, ShowTraffic = false, ShowWeather = false},
                new User { ID = 6, Username = "Nisselina" ,  Password = passHelper.HashPassword("delösersig", passHelper.GenerateSalt()), ShowAccidents = true, ShowPublicTransport = true, ShowTraffic = false, ShowWeather = true},
                new User { ID = 7, Username = "Kalle" ,  Password = passHelper.HashPassword("delösersig", passHelper.GenerateSalt()), ShowAccidents = false, ShowPublicTransport = false, ShowTraffic = true, ShowWeather = true},
                new User { ID = 8, Username = "Sean" , Password = passHelper.HashPassword("delösersig", passHelper.GenerateSalt()), ShowAccidents = false, ShowPublicTransport = false, ShowTraffic = false, ShowWeather = true},
                new User { ID = 9, Username = "Albert" , Password = passHelper.HashPassword("delösersiginte", passHelper.GenerateSalt()), ShowAccidents = false, ShowPublicTransport = false, ShowTraffic = false, ShowWeather = false},
                new User { ID = 10, Username = "Sofia" , Password = passHelper.HashPassword("delösersiginte", passHelper.GenerateSalt()), ShowAccidents = false, ShowPublicTransport = false, ShowTraffic = false, ShowWeather = false},
                new User { ID = 11, Username = "Victoria" , Password = passHelper.HashPassword("delösersiginte", passHelper.GenerateSalt()), ShowAccidents = true, ShowPublicTransport = true, ShowTraffic = true, ShowWeather = false},
                new User { ID = 12, Username = "Karin" , Password = passHelper.HashPassword("delösersiginte", passHelper.GenerateSalt()), ShowAccidents = false, ShowPublicTransport = false, ShowTraffic = true, ShowWeather = true},
                new User { ID = 13, Username = "Birger" , Password = passHelper.HashPassword("delösersiginte", passHelper.GenerateSalt()), ShowAccidents = false, ShowPublicTransport = true, ShowTraffic = false, ShowWeather = true},
                new User { ID = 14, Username = "Nisse" , Password = passHelper.HashPassword("delösersiginte", passHelper.GenerateSalt()), ShowAccidents = false, ShowPublicTransport = false, ShowTraffic = false, ShowWeather = true},
                new User { ID = 15, Username = "Alice" , Password = passHelper.HashPassword("delösersiginte", passHelper.GenerateSalt()), ShowAccidents = true, ShowPublicTransport = true, ShowTraffic = false, ShowWeather = true},
                new User { ID = 16, Username = "Algott" , Password = passHelper.HashPassword("delösersiginte", passHelper.GenerateSalt()), ShowAccidents = false, ShowPublicTransport = false, ShowTraffic = false, ShowWeather = true},
            };


            Users.ForEach(s => context.Users.AddOrUpdate(s));

            var TollLocations = new List<TollLocation>
            {
                new TollLocation {ID = 1, TrafikVID = 1, Name = "Fridkullagatan", Direction = "N", LaneNumbers = 1, PointLat = "57.682438", PointLong = "11.98774" },
                new TollLocation {ID = 2, TrafikVID = 1, Name = "Fridkullagatan", Direction = "S", LaneNumbers = 1, PointLat = "57.682422", PointLong = "11.987501" },
                new TollLocation {ID = 3, TrafikVID = 2, Name = "Gibraltargatan", Direction = "N", LaneNumbers = 1, PointLat = "57.683001", PointLong = "11.984609" },
                new TollLocation {ID = 4, TrafikVID = 2, Name = "Gibraltargatan", Direction = "S", LaneNumbers = 1, PointLat = "57.682991", PointLong = "11.984311" },
                new TollLocation {ID = 5, TrafikVID = 3, Name = "Doktor Allards gata ", Direction = "N", LaneNumbers = 1, PointLat = "57.681450", PointLong = "11.978789" },
                new TollLocation {ID = 6, TrafikVID = 3, Name = "Doktor Allards gata", Direction = "S", LaneNumbers = 1, PointLat = "57.681466", PointLong = "11.97856" },
                new TollLocation {ID = 7, TrafikVID = 4, Name = "Ehrenströmsgatan", Direction = "NW", LaneNumbers = 1, PointLat = "57.679239", PointLong = "11.96878" },
                new TollLocation {ID = 8, TrafikVID = 4, Name = "Ehrenströmsgatan", Direction = "SE", LaneNumbers = 1, PointLat = "57.679139", PointLong = "11.968581" },
                new TollLocation {ID = 9, TrafikVID = 5, Name = "Dag Hammarskjöldsleden", Direction = "NE", LaneNumbers = 2, PointLat = "57.677877", PointLong = "11.942431" },
                new TollLocation {ID = 10,TrafikVID = 5, Name = "Dag Hammarskjöldsleden", Direction = "SW", LaneNumbers = 3, PointLat = "57.678093", PointLong = "11.941995" },
                new TollLocation {ID = 11,TrafikVID = 6, Name = "Margaretebergsgatan", Direction = "E", LaneNumbers = 1, PointLat = "57.680722", PointLong = "11.94326" },
                new TollLocation {ID = 12,TrafikVID = 6, Name = "Margaretebergsgatan", Direction = "W", LaneNumbers = 1, PointLat = "57.680933", PointLong = "11.943161" },
                new TollLocation {ID = 13,TrafikVID = 7, Name = "Fjällgatan/Jungmansgatan", Direction = "SE", LaneNumbers = 1, PointLat = "57.696145", PointLong = "11.995908" },
                new TollLocation {ID = 14,TrafikVID = 8, Name = "Stigbergsliden", Direction = "E", LaneNumbers = 1, PointLat = "57.699376", PointLong = "11.936839" },
                new TollLocation {ID = 15,TrafikVID = 8, Name = "Stigbergsliden", Direction = "W", LaneNumbers = 1, PointLat = "57.699556", PointLong = "11.936339" },
                new TollLocation { ID = 16 ,TrafikVID = 9, Name = "E45 Oscarsleden", Direction = "E", LaneNumbers = 3, PointLat = "57.699874", PointLong = "11.935908" },
                new TollLocation {ID = 17 ,TrafikVID = 9, Name = "E45 Oscarsleden", Direction = "W", LaneNumbers = 2, PointLat = "57.700014", PointLong = "11.935875" },
                new TollLocation {ID = 18 ,TrafikVID = 10, Name = "Emigrantvägen", Direction = "E", LaneNumbers = 1, PointLat = "57.700096", PointLong = "11.935856" },
                new TollLocation {ID = 19 ,TrafikVID = 10, Name = "Emigrantvägen", Direction = "W", LaneNumbers = 1, PointLat = "57.700187", PointLong = "11.935834" },
                new TollLocation {ID = 20 ,TrafikVID = 11, Name = "Älvsborgsbron", Direction = "S", LaneNumbers = 3, PointLat = "57.694787", PointLong = "11.898929" },
                new TollLocation {ID = 21 ,TrafikVID = 11, Name = "Älvsborgsbron", Direction = "N", LaneNumbers = 3, PointLat = "57.694867", PointLong = "11.899345" },
                new TollLocation {ID = 22 ,TrafikVID = 12, Name = "Lindholmsallén", Direction = "E", LaneNumbers = 2, PointLat = "57.707664", PointLong = "11.935622" },
                new TollLocation {ID = 23 ,TrafikVID = 12, Name = "Lindholmsallén", Direction = "W", LaneNumbers = 2, PointLat = "57.707774", PointLong = "11.935626" },
                new TollLocation {ID = 24 ,TrafikVID = 13, Name = "Karlavagnsgatan västra", Direction = "NE", LaneNumbers = 1, PointLat = "57.708200", PointLong = "11.935344" },
                new TollLocation {ID = 25 ,TrafikVID = 13, Name = "Karlavagnsgatan västra", Direction = "SW", LaneNumbers = 1, PointLat = "57.708284", PointLong = "11.935167" },
                new TollLocation {ID = 26 ,TrafikVID = 14, Name = "Polstjärnegatan", Direction = "E", LaneNumbers = 1, PointLat = "57.710603", PointLong = "11.936474" },
                new TollLocation {ID = 27 ,TrafikVID = 15, Name = "Karlavagnsgatan östra", Direction = "SW", LaneNumbers = 2, PointLat = "57.711943", PointLong = "11.942861" },
                new TollLocation {ID = 28 ,TrafikVID = 15, Name = "Karlavagnsgatan östra", Direction = "NE", LaneNumbers = 1, PointLat = "57.711821", PointLong = "11.943096" },
                new TollLocation {ID = 29 ,TrafikVID = 16, Name = "Hjalmar Brantingsgatan", Direction = "W 58A", LaneNumbers = 4, PointLat = "57.720612", PointLong = "11.959080" },
                new TollLocation {ID = 30 ,TrafikVID = 16, Name = "Hjalmar Brantingsgatan", Direction = "W 58B", LaneNumbers = 0, PointLat = "57.720435", PointLong = "11.959071" },
                new TollLocation {ID = 31 ,TrafikVID = 16, Name = "Hjalmar Brantingsgatan", Direction = "SE 77A", LaneNumbers = 4, PointLat = "57.720158609100295", PointLong = "11.958092424418169" },
                new TollLocation {ID = 32 ,TrafikVID = 16, Name = "Hjalmar Brantingsgatan", Direction = "SE 77B", LaneNumbers = 0, PointLat = "57.720348073336694", PointLong = "11.958052394907629" },
                new TollLocation {ID = 33 ,TrafikVID = 17, Name = "Södra Tagenevägen", Direction = "SW", LaneNumbers = 1, PointLat = "57.75926428491832", PointLong = "11.988538173006384" },
                //new TollLocation {ID = 34 ,TrafikVID = 17, Name = "Södra Tagenevägen", Direction = "NE", LaneNumbers = 1, PointLat = "57.75921154054909", PointLong = "11.988738662641394" },
                new TollLocation {ID = 35 ,TrafikVID = 18, Name = "Skälltorpsvägen ", Direction = "W", LaneNumbers = 1, PointLat = "57.758280544550274", PointLong = "11.989628396368675" },
                //new TollLocation {ID = 36 ,TrafikVID = 18, Name = "Skälltorpsvägen ", Direction = "E", LaneNumbers = 2, PointLat = "57.75809262465981", PointLong = "11.989695133995665" },
                new TollLocation {ID = 37 ,TrafikVID = 19, Name = "Backadalen", Direction = "W", LaneNumbers = 2, PointLat = "57.74767313333069", PointLong = "11.988950170494999" },
                //new TollLocation {ID = 38 ,TrafikVID = 19, Name = "Backadalen", Direction = "E", LaneNumbers = 2, PointLat = "57.747489353375556", PointLong = "11.988907921312181" },
                //new TollLocation {ID = 39 ,TrafikVID = 20, Name = "Tingstadsmotet avfart E6", Direction = "SW 53A", LaneNumbers = 1, PointLat = "57.731972798691466", PointLong = "11.983366845197873" },
                new TollLocation {ID = 40 ,TrafikVID = 20, Name = "Tingstadsmotet avfart E6", Direction = "SW 53B", LaneNumbers = 0, PointLat = "57.732062309447635", PointLong = "11.983271124440083" },
                new TollLocation {ID = 41 ,TrafikVID = 21, Name = "Tingstadsvägen", Direction = "NW", LaneNumbers = 1, PointLat = "57.73103534881655", PointLong = "11.982585826950062" },
                //new TollLocation {ID = 42 ,TrafikVID = 21, Name = "Tingstadsvägen", Direction = "SE", LaneNumbers = 1, PointLat = "57.730885031419525", PointLong = "11.982491356271561" },
                //new TollLocation {ID = 43 ,TrafikVID = 22, Name = "Ringömotet", Direction = "W 73A", LaneNumbers = 2, PointLat = "57.7263465255761", PointLong = "11.976981920876046" },
                new TollLocation {ID = 44 ,TrafikVID = 22, Name = "Ringömotet", Direction = "W 73B", LaneNumbers = 0, PointLat = "57.72612248216565", PointLong = "11.977043459792204" },
                //new TollLocation {ID = 45 ,TrafikVID = 22, Name = "Ringömotet", Direction = "E 74A", LaneNumbers = 2, PointLat = "57.72573243558849", PointLong = "11.975370287624173" },
                new TollLocation {ID = 46 ,TrafikVID = 22, Name = "Ringömotet", Direction = "E 74B", LaneNumbers = 0, PointLat = "57.72585322621782", PointLong = "11.975397496388675" },
                new TollLocation {ID = 47 ,TrafikVID = 22, Name = "Ringömotet", Direction = "SE 75A", LaneNumbers = 4, PointLat = "57.72448835443453", PointLong = "11.98222582066893" },
                new TollLocation {ID = 48 ,TrafikVID = 22, Name = "Ringömotet", Direction = "SE 75B", LaneNumbers = 0, PointLat = "57.72460969350004", PointLong = "11.982505261800364" },
                new TollLocation {ID = 49 ,TrafikVID = 22, Name = "Ringömotet", Direction = "NW 76A", LaneNumbers = 5, PointLat = "57.72488646364264", PointLong = "11.983139678640537" },
                new TollLocation {ID = 50 ,TrafikVID = 22, Name = "Ringömotet", Direction = "NW 76B", LaneNumbers = 0, PointLat = "57.72473637978326", PointLong = "11.982795262832411" },
                new TollLocation {ID = 51 ,TrafikVID = 23, Name = "Salsmästaregatan", Direction = "SW", LaneNumbers = 1, PointLat = "57.72407706273325", PointLong = "11.984565263955968" },
                //new TollLocation {ID = 52 ,TrafikVID = 23, Name = "Salsmästaregatan", Direction = "NE", LaneNumbers = 1, PointLat = "57.72397983954825", PointLong = "11.98472019605677" },
                //new TollLocation {ID = 53 ,TrafikVID = 24, Name = "Marieholmsgatan", Direction = "SW", LaneNumbers = 1, PointLat = "57.72079396737468", PointLong = "11.990664563135889" },
                new TollLocation {ID = 54 ,TrafikVID = 24, Name = "Marieholmsgatan", Direction = "NE", LaneNumbers = 1, PointLat = "57.72071145006924", PointLong = "11.990867492279163" },
                new TollLocation {ID = 55 ,TrafikVID = 25, Name = "E45 Marieholmsleden", Direction = "SW 28A", LaneNumbers = 2, PointLat = "57.720101304400295", PointLong = "11.99357585794067" },
                new TollLocation {ID = 56 ,TrafikVID = 25, Name = "E45 Marieholmsleden", Direction = "SW 28B", LaneNumbers = 0, PointLat = "57.71996857641602", PointLong = "11.993887721853957" },
                new TollLocation {ID = 57 ,TrafikVID = 25, Name = "E45 Marieholmsleden", Direction = "NE 28C", LaneNumbers = 4, PointLat = "57.71991542768797", PointLong = "11.9940180749818" },
                new TollLocation {ID = 58 ,TrafikVID = 25, Name = "E45 Marieholmsleden", Direction = "NE 28D", LaneNumbers = 0, PointLat = "57.719862186690044", PointLong = "11.99422400449003" },
                new TollLocation {ID = 59 ,TrafikVID = 26, Name = "Partihandelsgatan", Direction = "SW", LaneNumbers = 1, PointLat = "57.718664716076695", PointLong = "11.994465647342707" },
                //new TollLocation {ID = 60 ,TrafikVID = 26, Name = "Partihandelsgatan", Direction = "NE", LaneNumbers = 1, PointLat = "57.718564663011584", PointLong = "11.99457532944723" },
                new TollLocation {ID = 61 ,TrafikVID = 27, Name = "E20 Alingsåsleden", Direction = "SW 26C", LaneNumbers = 3, PointLat = "57.71687764496254", PointLong = "11.996708481854997" },
                new TollLocation {ID = 62 ,TrafikVID = 27, Name = "E20 Alingsåsleden", Direction = "C 26B", LaneNumbers = 0, PointLat = "57.71677440487059", PointLong = "11.996931617367181" },
                new TollLocation {ID = 63 ,TrafikVID = 27, Name = "E20 Alingsåsleden", Direction = "NE 26A", LaneNumbers = 3, PointLat = "57.716679951202565", PointLong = "11.997136438655096" },
                new TollLocation {ID = 64 ,TrafikVID = 28, Name = "Olskroksmotet avfart E20", Direction = "E 25A", LaneNumbers = 1, PointLat = "57.715286412806655", PointLong = "11.995036730225246" },
                new TollLocation {ID = 65 ,TrafikVID = 28, Name = "Olskroksmotet avfart E20", Direction = "E 25B", LaneNumbers = 0, PointLat = "57.715175373516", PointLong = "11.995039573276745" },
                new TollLocation {ID = 66 ,TrafikVID = 29, Name = "Olskroksmotet påfart E6", Direction = "N", LaneNumbers = 1, PointLat = "57.71480260588332", PointLong = "11.995019900542793" },
                new TollLocation {ID = 67 ,TrafikVID = 30, Name = "Redbergsvägen", Direction = "W ", LaneNumbers = 1, PointLat = "57.71419272697944", PointLong = "11.99515745226581" },
                new TollLocation {ID = 68 ,TrafikVID = 30, Name = "Redbergsvägen", Direction = "E", LaneNumbers = 2, PointLat = "57.71401515227095", PointLong = "11.995650843738888" },
                new TollLocation {ID = 69 ,TrafikVID = 32, Name = "Örgrytevägen", Direction = "W 21A", LaneNumbers = 3, PointLat = "57.69795331584298", PointLong = "11.997192311284083" },
                new TollLocation {ID = 70 ,TrafikVID = 32, Name = "Örgrytevägen", Direction = "W 21B", LaneNumbers = 0, PointLat = "57.69784282209245", PointLong = "11.997234799820081" },
                new TollLocation {ID = 71 ,TrafikVID = 32, Name = "Örgrytevägen", Direction = "E 21C", LaneNumbers = 2, PointLat = "57.697758431224784", PointLong = "11.997272088134553" },
                new TollLocation {ID = 72 ,TrafikVID = 32, Name = "Örgrytevägen", Direction = "E 21D", LaneNumbers = 0, PointLat = "57.697620491804706", PointLong = "11.99732682011278" },
                new TollLocation {ID = 73 ,TrafikVID = 33, Name = "E6/E20 Kungsbackaleden", Direction = "NW 70A", LaneNumbers = 2, PointLat = "57.689831743085655", PointLong = "12.000544932470113" },
                new TollLocation {ID = 74 ,TrafikVID = 33, Name = "E6/E20 Kungsbackaleden", Direction = "NW 70B", LaneNumbers = 0, PointLat = "57.689929247121796", PointLong = "12.000735378211344" },
                new TollLocation {ID = 75 ,TrafikVID = 33, Name = "E6/E20 Kungsbackaleden", Direction = "SE 71A", LaneNumbers = 2, PointLat = "57.68891176250445", PointLong = "12.001185804801597" },
                new TollLocation {ID = 76 ,TrafikVID = 33, Name = "E6/E20 Kungsbackaleden", Direction = "SE 71B", LaneNumbers = 0, PointLat = "57.6890349793579", PointLong = "12.00129739207191" },
                new TollLocation {ID = 77 ,TrafikVID = 33, Name = "E6/E20 Kungsbackaleden", Direction = "NW", LaneNumbers = 2, PointLat = "57.688539853011", PointLong = "12.001351649056945" },
                new TollLocation {ID = 78 ,TrafikVID = 33, Name = "E6/E20 Kungsbackaleden", Direction = "SE", LaneNumbers = 2, PointLat = "57.68842600837622", PointLong = "12.000827216762703" },
                new TollLocation {ID = 79 ,TrafikVID = 34, Name = "Sankt Sigfridsgatan", Direction = "N", LaneNumbers = 1, PointLat = "57.689169536703815", PointLong = "12.005053287257384" },
                new TollLocation {ID = 80 ,TrafikVID = 34, Name = "Sankt Sigfridsgatan", Direction = "S", LaneNumbers = 1, PointLat = "57.68952062352559", PointLong = "12.004867228917291" },
                new TollLocation {ID = 81 ,TrafikVID = 35, Name = "Almedalsvägen", Direction = "N", LaneNumbers = 1, PointLat = "57.68588610470052", PointLong = "12.002259637287656" },
                new TollLocation {ID = 82 ,TrafikVID = 35, Name = "Almedalsvägen", Direction = "S", LaneNumbers = 1, PointLat = "57.68583604031368", PointLong = "12.002058961604117" },
                new TollLocation {ID = 83 ,TrafikVID = 36, Name = "Mölndalsvägen", Direction = "N", LaneNumbers = 2, PointLat = "57.68507497633927", PointLong = "11.999608607742605" },
                new TollLocation {ID = 84 ,TrafikVID = 36, Name = "Mölndalsvägen", Direction = "S", LaneNumbers = 2, PointLat = "57.685001461433835", PointLong = "11.999239874472936" },
            };
            TollLocations.ForEach(s => context.TollLocations.AddOrUpdate(s));

            var bingMapWebService = new BingMapTrafficWebService("AoWk0xixw7Xr16xE6Tne-3nNsYihl9ab7yIhnoASonYm2sWCdYk7VNhhAUg82cUj");
            bingMapWebService.MakeRequest();
            var trafficIncidents = bingMapWebService.GetParsedResponse();
            if (trafficIncidents.Any())
            {
                trafficIncidents.ForEach(x => context.TrafficIncidents.AddOrUpdate(x));
            }

            var yrWheatherService = new YrWeatherWebService();
            yrWheatherService.MakeRequest();
            var wheatherPeriods = yrWheatherService.GetParsedResponse();
            if (wheatherPeriods.Any())
            {
                wheatherPeriods.ForEach(x => context.WheatherPeriods.AddOrUpdate(x));
            }

            var vasttrafikTraffickWebService = new VasttrafikTrafficWebService("myKey");
            vasttrafikTraffickWebService.MakeRequest();
            var vasttrafikIncidents = vasttrafikTraffickWebService.GetParsedResponse();
            if (vasttrafikIncidents.Any())
            {
                vasttrafikIncidents.ForEach(x => context.VasttrafikIncidents.AddOrUpdate(x));
            }

            

            var mapQuestDirectionsWebService = new MapQuestDirectionsWebService();
            mapQuestDirectionsWebService.TakeTrafficIncident(trafficIncidents);
            mapQuestDirectionsWebService.MakeRequest();
            var mapQuestDirections = mapQuestDirectionsWebService.GetParsedResponse();
            if (mapQuestDirections.Any())
            {
                mapQuestDirections.ForEach(x => context.MapQuestDirections.AddOrUpdate(x));
            }



            context.SaveChanges();
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
