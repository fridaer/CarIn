using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarIn.Models.Entities.Abstract;

namespace CarIn.Models.Entities
{
    public class TrafficIncident : IEntity
    {
        public int ID { get; set; }
        //Long
        public string IncidentId { get; set; }
        //Double
        public string PointLat { get; set; }
        public string PointLong { get; set; }
        public string ToPointLat { get; set; }
        public string ToPointLong { get; set; }

        //UTC Time JSOS ex: Date(1295704800000)
        public string Start { get; set; }
        public string End { get; set; }
        //DateTime
        public string LastModified { get; set; }

        //string
        public string Description { get; set; }
        public string Lane { get; set; }
        //int 
        //1: LowImpact 2: Minor 3: Moderate 4: Serious
        public string Severity { get; set; }

        // 1: Accident 2: Congestion 3: DisabledVehicle 4: MassTransit 5: Miscellaneous 
        // 6: OtherNews 7: PlannedEvent 8: RoadHazard 9: Construction 10: Alert 11: Weather
        public string IncidentType { get; set; }

        //bool
        public string RoadClosed { get; set; }
        public string Verified { get; set; }


    }
}