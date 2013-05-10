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

            context.SaveChanges();
        }
    }
}