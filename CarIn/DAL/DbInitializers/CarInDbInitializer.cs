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
    public class CarInDbInitializer : DropCreateDatabaseAlways<CarInContext>
    {

        protected override void Seed(CarInContext context)
        {
            var passHelper = new PasswordHelper();
            var Users = new List<User>
            {

                new User { Username = "Tobiasen" , Password = passHelper.HashPassword("delösersig", passHelper.GenerateSalt())},
                new User { Username = "Björn" , Password = passHelper.HashPassword("delösersig", passHelper.GenerateSalt())},
                new User { Username = "Frida" , Password = passHelper.HashPassword("delösersig", passHelper.GenerateSalt())},
                new User { Username = "Fredrik" ,  Password = passHelper.HashPassword("delösersig", passHelper.GenerateSalt())},
                new User { Username = "Urban" ,  Password = passHelper.HashPassword("delösersig", passHelper.GenerateSalt())},
                new User { Username = "Nisselina" ,  Password = passHelper.HashPassword("delösersig", passHelper.GenerateSalt())},
                new User { Username = "Kalle" ,  Password = passHelper.HashPassword("delösersig", passHelper.GenerateSalt())},
                new User { Username = "Sean" , Password = passHelper.HashPassword("delösersig", passHelper.GenerateSalt())},
                new User { Username = "Albert" , Password = passHelper.HashPassword("delösersiginte", passHelper.GenerateSalt())},
                new User { Username = "Sofia" , Password = passHelper.HashPassword("delösersiginte", passHelper.GenerateSalt())},
                new User { Username = "Victoria" , Password = passHelper.HashPassword("delösersiginte", passHelper.GenerateSalt())},
                new User { Username = "Karin" , Password = passHelper.HashPassword("delösersiginte", passHelper.GenerateSalt())},
                new User { Username = "Birger" , Password = passHelper.HashPassword("delösersiginte", passHelper.GenerateSalt())},
                new User { Username = "Nisse" , Password = passHelper.HashPassword("delösersiginte", passHelper.GenerateSalt())},
                new User { Username = "Alice" , Password = passHelper.HashPassword("delösersiginte", passHelper.GenerateSalt())},
                new User { Username = "Algott" , Password = passHelper.HashPassword("delösersiginte", passHelper.GenerateSalt())},
            };


            Users.ForEach(s => context.Users.Add(s));

            context.SaveChanges();
        }
    }
}