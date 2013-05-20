using CarIn.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace CarIn.BLL
{
    public class MapQuestDirectionsWebService
    {

        public List<MapQuestDirection> MakeRequest()
        {
            //string.Format("http://www.mapquestapi.com/directions/v1/route?key=Fmjtd%7Cluub2du8n1%2C2g%3Do5-9u2xg4&=renderAdvancedNarrative&ambiguities=ignore&avoidTimedConditions=false&doReverseGeocode=false&outFormat=json&routeType=shortest&timeType=0&enhancedNarrative=false&shapeFormat=raw&generalize=0&locale=sv_SE&unit=m&from=" + this.PointLat + "," + this.PointLong + "&to= " + this.ToPointLat + "," + this.ToPointLong + "&drivingStyle=2&highwayEfficiency=21.0");
            var MapQuestRequestURL = string.Format("http://www.mapquestapi.com/directions/v1/route?key=Fmjtd%7Cluub2du8n1%2C2g%3Do5-9u2xg4&=renderAdvancedNarrative&ambiguities=ignore&avoidTimedConditions=false&doReverseGeocode=false&outFormat=json&routeType=shortest&timeType=0&enhancedNarrative=false&shapeFormat=raw&generalize=0&locale=sv_SE&unit=m&from=57.7036,11.8602&to=57.71796,11.81875&drivingStyle=2&highwayEfficiency=21.0");

            HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(MapQuestRequestURL);
            Request.Method = WebRequestMethods.Http.Get;
            Request.Accept = "application/json";

            List<MapQuestDirection> test = new List<MapQuestDirection>();
            return test;
        }
    }
}