using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using CarIn.DAL.DbContext;

namespace CarIn.DAL.DbInitializers
{
    public class CarIn_DbInitializer : DropCreateDatabaseIfModelChanges<CarIn_Context>
    {
        protected override void Seed(CarIn_Context context)
        {

            base.Seed(context);
        }
    }
}