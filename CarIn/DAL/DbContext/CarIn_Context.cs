using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using CarIn.Models.Entities;

namespace CarIn.DAL.DbContext
{
    public class CarIn_Context : System.Data.Entity.DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}