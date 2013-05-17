using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarIn.Models.Entities;

namespace CarIn.Models.ViewModels
{
    public class MapInfoVm
    {
        public List<TrafficIncident> TrafficIncidents { get; set; }
        public List<WheatherPeriod> WheatherPeriods { get; set; }
        public List<VasttrafikIncident> VasttrafikIncidents { get; set; }
 
    }
}