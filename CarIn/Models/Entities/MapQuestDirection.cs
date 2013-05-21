using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarIn.Models.Entities.Abstract;

namespace CarIn.Models.Entities
{
    public class MapQuestDirection : IEntity
    {
        public int ID { get; set; }
        public Array shapePoints { get; set; }
        public string PointLat { get; set; }
        public string PointLong { get; set; }
        public string ToPointLat { get; set; }
        public string ToPointLong { get; set; }
    }
}