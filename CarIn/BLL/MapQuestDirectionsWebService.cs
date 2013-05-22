using CarIn.Models.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;

namespace CarIn.BLL
{
    public class routeObj
    {
        public jsonObject route { get; set; }
    }
    
    public class jsonObject
    {
        public object hasTollRoad { get; set; }
        public object fuelUsed { get; set; }
        public shapeObj shape { get; set; }
    }
    public class shapeObj
    {
        public List<float> shapePoints { get; set; }

    }
   
   
    public class MapQuestDirectionsWebService
    {

        public MapQuestDirection MakeRequest()
        {
            //string.Format("http://www.mapquestapi.com/directions/v1/route?key=Fmjtd%7Cluub2du8n1%2C2g%3Do5-9u2xg4&=renderAdvancedNarrative&ambiguities=ignore&avoidTimedConditions=false&doReverseGeocode=false&outFormat=json&routeType=shortest&timeType=0&enhancedNarrative=false&shapeFormat=raw&generalize=0&locale=sv_SE&unit=m&from=" + this.PointLat + "," + this.PointLong + "&to= " + this.ToPointLat + "," + this.ToPointLong + "&drivingStyle=2&highwayEfficiency=21.0");
            var MapQuestRequestURL = string.Format("http://www.mapquestapi.com/directions/v1/route?key=Fmjtd%7Cluub2du8n1%2C2g%3Do5-9u2xg4&=renderAdvancedNarrative&ambiguities=ignore&avoidTimedConditions=false&doReverseGeocode=false&outFormat=json&routeType=shortest&timeType=0&enhancedNarrative=false&shapeFormat=raw&generalize=0&locale=sv_SE&unit=m&from=57.7036,11.8602&to=57.71796,11.81875&drivingStyle=2&highwayEfficiency=21.0");

            HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(MapQuestRequestURL);
            Request.Method = WebRequestMethods.Http.Get;
            Request.Accept = "application/json";
            string text;
            routeObj json;
            var response = (HttpWebResponse)Request.GetResponse();

            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                text = sr.ReadToEnd();
                json = (routeObj)js.Deserialize(text, typeof(routeObj));
            }
            var StringOfLatlong = "";
            var i=0;

            //Formatted as 58,1759649997499.11.4035799936928;
            do
            {
                StringOfLatlong += json.route.shape.shapePoints[i].ToString() + ".";
                StringOfLatlong += json.route.shape.shapePoints[i + 1].ToString() + ";";
                i++;
            }
            while (json.route.shape.shapePoints.Count-1 > i);
            MapQuestDirection Direction = new MapQuestDirection();

            return Direction;
        }
    }
}