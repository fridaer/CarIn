using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using CarIn.Models.Entities;

namespace CarIn.DAL.Context
{
    public class CarInContext : System.Data.Entity.DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<TollLocation> TollLocations { get; set; }
        public DbSet<TrafficIncident> TrafficIncidents { get; set; }
        public DbSet<WheatherPeriod> WheatherPeriods { get; set; }
        public DbSet<VasttrafikIncident> VasttrafikIncidents { get; set; }
        public DbSet<WebServiceLogg> WebServiceLoggs { get; set; }
        public DbSet<MapQuestDirection> MapQuestDirections { get; set; } 
        

    }
}