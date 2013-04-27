using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using CarIn.DAL.Context;

namespace CarIn.DAL.DbInitializers
{
    public class CarInDbInitializer : DropCreateDatabaseIfModelChanges<CarInContext>
    {
        protected override void Seed(CarInContext context)
        {

            base.Seed(context);
        }
    }
}