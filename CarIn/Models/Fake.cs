using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarIn.Models.Entities.Abstract;

namespace CarIn.Models
{
    public class Fake : IEntity
    {
        public int ID { get; set; }
    }
}