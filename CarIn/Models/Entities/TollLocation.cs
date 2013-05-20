using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarIn.Models.Entities.Abstract;

namespace CarIn.Models.Entities
{
    public class TollLocation : IEntity
    {
        public int ID { get; set; }
        public int TrafikVID { get; set; }

        public string Name { get; set; }
        public string Direction { get; set; }
        public int LaneNumbers { get; set; }

        public string PointLong { get; set; }
        public string PointLat { get; set; }
    }
}