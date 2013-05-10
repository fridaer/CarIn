using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarIn.Models.Entities.Abstract;

namespace CarIn.Models.Entities
{
    public class WheatherPeriod : IEntity
    {
        public int ID { get; set; }
        //int
        public string PeriodNumber { get; set; }
        public string SymbolName { get; set; }
        public string WindCode { get; set; }
        public string WindSpeedMps { get; set; }
        public string TemperatureCelsius { get; set; }

    }
}