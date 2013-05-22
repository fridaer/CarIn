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
        public string shapePoints { get; set; }
    }
}