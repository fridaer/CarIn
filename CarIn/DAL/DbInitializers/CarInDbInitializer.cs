using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using CarIn.DAL.Context;
using CarIn.Models.Entities;

namespace CarIn.DAL.DbInitializers
{
    public class CarInDbInitializer : DropCreateDatabaseAlways<CarInContext>
    {

        protected override void Seed(CarInContext context)
        {
            var Users = new List<User>
            {
                new User { ID = 1, Username = "Tobiasen" , Password = "delösersig"},
                new User { ID = 2, Username = "Björn" , Password = "delösersig"},
                new User { ID = 3, Username = "Frida" , Password = "delösersig"},
                new User { ID = 4, Username = "Fredrik" , Password = "delösersig"},
                new User { ID = 5, Username = "Urban" , Password = "delösersig"},
                new User { ID = 6, Username = "Nisselina" , Password = "delösersig"},
                new User { ID = 7, Username = "Kalle" , Password = "delösersig"},
                new User { ID = 8, Username = "Sean" , Password = "delösersig"},
                new User { ID = 9, Username = "Albert" , Password = "delösersiginte"},
                new User { ID = 10, Username = "Sofia" , Password = "delösersiginte"},
                new User { ID = 11, Username = "Victoria" , Password = "delösersiginte"},
                new User { ID = 12, Username = "Karin" , Password = "delösersiginte"},
                new User { ID = 13, Username = "Birger" , Password = "delösersiginte"},
                new User { ID = 14, Username = "Nisse" , Password = "delösersiginte"},
                new User { ID = 15, Username = "Alice" , Password = "delösersiginte"},
                new User { ID = 16, Username = "Algott" , Password = "delösersiginte"},
            };


            Users.ForEach(s => context.Users.Add(s));

            context.SaveChanges();
        }
    }
}