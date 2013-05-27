using CarIn.DAL.Context;
using CarIn.DAL.Repositories.Abstract;
using CarIn.Models.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
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
        public shapeObj shape { get; set; }
    }

    public class shapeObj
    {
        public List<float> shapePoints { get; set; }
    }

    public class MapQuestDirectionsWebService
    {

        public String MakeUrl(string PointLat, string PointLong, string ToPointLat, string ToPointLong)
        {
            var MapQuestRequestURL = "http://www.mapquestapi.com/directions/v1/route?key=Fmjtd%7Cluub2du8n1%2C2g%3Do5-9u2xg4&=renderAdvancedNarrative&ambiguities=ignore&avoidTimedConditions=false&doReverseGeocode=false&outFormat=json&routeType=shortest&timeType=0&enhancedNarrative=false&shapeFormat=raw&generalize=0&locale=sv_SE&unit=m&from=" + PointLat + "," + PointLong + "&to=" + ToPointLat + "," + ToPointLong;
            return MapQuestRequestURL;
        }

        public List<MapQuestDirection> MakeRequestForeachIncedent(List<TrafficIncident> TrafficIncidents)
        {
            List<MapQuestDirection> MapQuestDirections = new List<MapQuestDirection>();
            foreach(TrafficIncident item in TrafficIncidents){
                var MapQuestRequestURL = MakeUrl(item.PointLat, item.PointLong, item.ToPointLat, item.ToPointLong);
                var json = GetResponse(MapQuestRequestURL);
                MapQuestDirection direction = new MapQuestDirection();
                direction.shapePoints = ShapePointsToString(json);
                MapQuestDirections.Add(direction);
            }

            return MapQuestDirections;
        }
        public routeObj GetResponse(String URL) {

            HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(URL);
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

            return json;
        }

        public List<MapQuestDirection> MakeRequest(List<TrafficIncident> TrafficIncidents)
        {

            var TrafficIncedentsWithEndPoints = FindTrafficIncedentsWithEndPoints(TrafficIncidents);
            var Directions = MakeRequestForeachIncedent(TrafficIncedentsWithEndPoints);
            return Directions;
        }

        public string ShapePointsToString(routeObj json)
        {

            //Formatted as 58,1759649997499.11.4035799936928;
            var StringOfLatlong = "";
            var latindexer=0;
            var longindexer=1;
            do
            {
                StringOfLatlong += json.route.shape.shapePoints[latindexer].ToString() + ".";
                StringOfLatlong += json.route.shape.shapePoints[longindexer].ToString() + ";";
                latindexer=  latindexer + 2;
                longindexer = longindexer + 2;

            }
            while (json.route.shape.shapePoints.Count - 1 > longindexer);

            return StringOfLatlong;
        }

        public List<TrafficIncident> FindTrafficIncedentsWithEndPoints(List<TrafficIncident> trafficIncidents)
        {


            List<TrafficIncident> TrafficIncedentsWithEndPoints = new List<TrafficIncident>();

            foreach (TrafficIncident item in trafficIncidents)
            {

                if (item.PointLat != item.ToPointLat || item.PointLong != item.ToPointLong)
                {
                    TrafficIncedentsWithEndPoints.Add(item);   
                }
            }

            return TrafficIncedentsWithEndPoints;
        }
    }
}