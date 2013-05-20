using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using CarIn.DAL.Context;
using CarIn.Models.Entities;
using CarIn.BLL;

namespace CarIn.DAL.DbInitializers
{
    public class CarInDbInitializer : DropCreateDatabaseIfModelChanges<CarInContext>
    {

        protected override void Seed(CarInContext context)
        {
            var passHelper = new PasswordHelper();
            var Users = new List<User>
            
            {

                new User { Username = "Tobiasen" , Password = passHelper.HashPassword("delösersig", passHelper.GenerateSalt()), ShowAccidents = false, ShowPublicTransport = false, ShowTraffic = false, ShowWeather = true},
                new User { Username = "Björn" , Password = passHelper.HashPassword("delösersig", passHelper.GenerateSalt()), ShowAccidents = true, ShowPublicTransport = false, ShowTraffic = false, ShowWeather = true},
                new User { Username = "Frida" , Password = passHelper.HashPassword("delösersig", passHelper.GenerateSalt()), ShowAccidents = true, ShowPublicTransport = false, ShowTraffic = false, ShowWeather = false},
                new User { Username = "Fredrik" ,  Password = passHelper.HashPassword("delösersig", passHelper.GenerateSalt()), ShowAccidents = false, ShowPublicTransport = true, ShowTraffic = false, ShowWeather = false},
                new User { Username = "Urban" ,  Password = passHelper.HashPassword("delösersig", passHelper.GenerateSalt()), ShowAccidents = false, ShowPublicTransport = false, ShowTraffic = false, ShowWeather = false},
                new User { Username = "Nisselina" ,  Password = passHelper.HashPassword("delösersig", passHelper.GenerateSalt()), ShowAccidents = true, ShowPublicTransport = true, ShowTraffic = false, ShowWeather = true},
                new User { Username = "Kalle" ,  Password = passHelper.HashPassword("delösersig", passHelper.GenerateSalt()), ShowAccidents = false, ShowPublicTransport = false, ShowTraffic = true, ShowWeather = true},
                new User { Username = "Sean" , Password = passHelper.HashPassword("delösersig", passHelper.GenerateSalt()), ShowAccidents = false, ShowPublicTransport = false, ShowTraffic = false, ShowWeather = true},
                new User { Username = "Albert" , Password = passHelper.HashPassword("delösersiginte", passHelper.GenerateSalt()), ShowAccidents = false, ShowPublicTransport = false, ShowTraffic = false, ShowWeather = false},
                new User { Username = "Sofia" , Password = passHelper.HashPassword("delösersiginte", passHelper.GenerateSalt()), ShowAccidents = false, ShowPublicTransport = false, ShowTraffic = false, ShowWeather = false},
                new User { Username = "Victoria" , Password = passHelper.HashPassword("delösersiginte", passHelper.GenerateSalt()), ShowAccidents = true, ShowPublicTransport = true, ShowTraffic = true, ShowWeather = false},
                new User { Username = "Karin" , Password = passHelper.HashPassword("delösersiginte", passHelper.GenerateSalt()), ShowAccidents = false, ShowPublicTransport = false, ShowTraffic = true, ShowWeather = true},
                new User { Username = "Birger" , Password = passHelper.HashPassword("delösersiginte", passHelper.GenerateSalt()), ShowAccidents = false, ShowPublicTransport = true, ShowTraffic = false, ShowWeather = true},
                new User { Username = "Nisse" , Password = passHelper.HashPassword("delösersiginte", passHelper.GenerateSalt()), ShowAccidents = false, ShowPublicTransport = false, ShowTraffic = false, ShowWeather = true},
                new User { Username = "Alice" , Password = passHelper.HashPassword("delösersiginte", passHelper.GenerateSalt()), ShowAccidents = true, ShowPublicTransport = true, ShowTraffic = false, ShowWeather = true},
                new User { Username = "Algott" , Password = passHelper.HashPassword("delösersiginte", passHelper.GenerateSalt()), ShowAccidents = false, ShowPublicTransport = false, ShowTraffic = false, ShowWeather = true},
            };
            Users.ForEach(s => context.Users.Add(s));

            var TollLocations = new List<TollLocation>
            {
                new TollLocation { TrafikVID =  1, Name ="Fridkullagatan", Direction ="N", LaneNumbers =  1, PointLat ="57,682438", PointLong ="11,98774"},
                new TollLocation { TrafikVID =  1, Name ="Fridkullagatan", Direction ="S", LaneNumbers =  1, PointLat ="57,682422", PointLong ="11,987501"},
                new TollLocation { TrafikVID =  2, Name ="Gibraltargatan", Direction ="N", LaneNumbers =  1, PointLat ="57,683001", PointLong ="11,984609"},
                new TollLocation { TrafikVID =  2, Name ="Gibraltargatan", Direction ="S", LaneNumbers =  1, PointLat ="57,682991", PointLong ="11,984311"},
                new TollLocation { TrafikVID =  3, Name ="Doktor Allards gata ", Direction ="N", LaneNumbers =  1, PointLat ="57,681450", PointLong ="11,978789"},
                new TollLocation { TrafikVID =  3, Name ="Doktor Allards gata", Direction ="S", LaneNumbers =  1, PointLat ="57,681466", PointLong ="11,97856"},
                new TollLocation { TrafikVID =  4, Name ="Ehrenströmsgatan", Direction ="NW", LaneNumbers =  1, PointLat ="57,679239", PointLong ="11,96878"},
                new TollLocation { TrafikVID =  4, Name ="Ehrenströmsgatan", Direction ="SE", LaneNumbers =  1, PointLat ="57,679139", PointLong ="11,968581"},
                new TollLocation { TrafikVID =  5, Name ="Dag Hammarskjöldsleden", Direction ="NE", LaneNumbers =  2, PointLat ="57,677877", PointLong ="11,942431"},
                new TollLocation { TrafikVID =  5, Name ="Dag Hammarskjöldsleden", Direction ="SW", LaneNumbers =  3, PointLat ="57,678093", PointLong ="11,941995"},
                new TollLocation { TrafikVID =  6, Name ="Margaretebergsgatan", Direction ="E", LaneNumbers =  1, PointLat ="57,680722", PointLong ="11,94326"},
                new TollLocation { TrafikVID =  6, Name ="Margaretebergsgatan", Direction ="W", LaneNumbers =  1, PointLat ="57,680933", PointLong ="11,943161"},
                new TollLocation { TrafikVID =  7, Name ="Fjällgatan/Jungmansgatan", Direction ="SE", LaneNumbers =  1, PointLat ="57,696145", PointLong ="11,995908"},
            };
            TollLocations.ForEach(s => context.TollLocations.Add(s));

            var bingMapWebService = new BingMapTrafficWebService("AoWk0xixw7Xr16xE6Tne-3nNsYihl9ab7yIhnoASonYm2sWCdYk7VNhhAUg82cUj");
            var trafficIncidents = bingMapWebService.MakeRequest();
            if(trafficIncidents.Any())
            {
                trafficIncidents.ForEach(x => context.TrafficIncidents.Add(x));
            }

            var yrWheatherService = new YrWeatherWebService();
            var wheatherPeriods = yrWheatherService.MakeRequest();
            if(wheatherPeriods.Any())
            {
                wheatherPeriods.ForEach(x => context.WheatherPeriods.Add(x));
            }

            var vasttrafikTraffickWebService = new VasttrafikTrafficWebService();
            var vasttrafikIncidents = vasttrafikTraffickWebService.MakeRequest();
            if (vasttrafikIncidents.Any())
            {
                vasttrafikIncidents.ForEach(x => context.VasttrafikIncidents.Add(x));
            }


            context.SaveChanges();
        }
    }
}