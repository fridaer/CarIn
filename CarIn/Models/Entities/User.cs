using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarIn.Models.Entities.Abstract;

namespace CarIn.Models.Entities
{
    public class User : IEntity
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public bool ShowWeather { get; set; }
        public bool ShowAccidents { get; set; }
        public bool ShowTraffic { get; set; }
        public bool ShowPublicTransport { get; set; }


    }
}