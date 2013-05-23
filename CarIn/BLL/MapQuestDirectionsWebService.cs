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

        public List<MapQuestDirection> ForeachIncedent(List<TrafficIncident> TrafficIncidents)
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

        public List<MapQuestDirection> MakeRequest()
        {

            var TrafficIncedentsWithEndPoints = GetTrafficIncedentsWithEndPoints();
            var Directions = ForeachIncedent(TrafficIncedentsWithEndPoints);
            return Directions;
        }

        public string ShapePointsToString(routeObj json)
        {

            //Formatted as 58,1759649997499.11.4035799936928;
            var StringOfLatlong = "";
            var i = 0;
            do
            {
                StringOfLatlong += json.route.shape.shapePoints[i].ToString() + ".";
                StringOfLatlong += json.route.shape.shapePoints[i + 1].ToString() + ";";
                i++;
            }
            while (json.route.shape.shapePoints.Count - 1 > i);

            return StringOfLatlong;
        }

        public List<TrafficIncident> GetTrafficIncedentsWithEndPoints()
        {

            var bingMapWebService = new BingMapTrafficWebService("AoWk0xixw7Xr16xE6Tne-3nNsYihl9ab7yIhnoASonYm2sWCdYk7VNhhAUg82cUj");
            List<TrafficIncident> trafficIncidents = bingMapWebService.MakeRequest();

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