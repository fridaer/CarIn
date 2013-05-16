using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarIn.Models.Entities.Abstract;

namespace CarIn.Models.Entities
{
    public class VasttrafikIncidents :IEntity
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Line { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public string Priority { get; set; }
        public string TrafficChangesCoords { get; set; }
    }
}