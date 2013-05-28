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
using CarIn.BLL.Abstract;

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


    public class MapQuestDirectionsWebService : IWebService<MapQuestDirection>
    {

        private List<MapQuestDirection> _mapQuestDirections = new List<MapQuestDirection>();
        private List<TrafficIncident> _trafficIncidents = new List<TrafficIncident>();

        public void TakeTrafficIncident(List<TrafficIncident> TrafficIncidents)
        {
            _trafficIncidents = TrafficIncidents;
        }

        public String MakeUrl(string PointLat, string PointLong, string ToPointLat, string ToPointLong)
        {
            var MapQuestRequestURL = "http://www.mapquestapi.com/directions/v1/route?key=Fmjtd%7Cluub2du8n1%2C2g%3Do5-9u2xg4&=renderAdvancedNarrative&ambiguities=ignore&avoidTimedConditions=false&doReverseGeocode=false&outFormat=json&routeType=shortest&timeType=0&enhancedNarrative=false&shapeFormat=raw&generalize=0&locale=sv_SE&unit=m&from=" + PointLat + "," + PointLong + "&to=" + ToPointLat + "," + ToPointLong;
            return MapQuestRequestURL;
        }

        public bool MakeRequest()
        {

            try
            {
                var TrafficIncedentsWithEndPoints = FindTrafficIncedentsWithEndPoints(_trafficIncidents);

                foreach (TrafficIncident item in TrafficIncedentsWithEndPoints)
                {
                    var MapQuestRequestURL = MakeUrl(item.PointLat, item.PointLong, item.ToPointLat, item.ToPointLong);

                    var request = (HttpWebRequest)WebRequest.Create(MapQuestRequestURL);
                    request.Method = WebRequestMethods.Http.Get;
                    request.Accept = "text/xml";
                    GetResponse(request);
                }
                return true;
            }
            catch {
                return false;
            }
            
        }

        public List<MapQuestDirection> GetParsedResponse()
        {

            return _mapQuestDirections;
        }

        public void LogEvents(HttpStatusCode statusCode, string statusMessage)
        {
            LoggHelper.SetLogg("MapQuestDirectionsWebService", statusCode.ToString(), statusMessage);
        }

        public bool GetResponse(HttpWebRequest request)
        {
            try
            {
                request.Method = WebRequestMethods.Http.Get;
                request.Accept = "application/json";
                string text;
                routeObj json;
                var response = (HttpWebResponse)request.GetResponse();

                using (var sr = new StreamReader(response.GetResponseStream()))
                {
                    LogEvents(response.StatusCode, response.StatusDescription);

                    JavaScriptSerializer js = new JavaScriptSerializer();
                    text = sr.ReadToEnd();
                    json = (routeObj)js.Deserialize(text, typeof(routeObj));
                }
                ShapePointsToString(json);

                MapQuestDirection direction = new MapQuestDirection();

                direction.shapePoints = ShapePointsToString(json);
                _mapQuestDirections.Add(direction);
                return true;
            }
            catch (WebException ex)
            {
                if (ex.Status != WebExceptionStatus.ProtocolError)
                {
                    LogEvents(HttpStatusCode.InternalServerError, "Exceptions is not ProtocolError");
                }
                else
                {
                    var response = ex.Response as HttpWebResponse;
                    if (response != null)
                    {
                        LogEvents(response.StatusCode, ex.Message);
                    }
                    else
                    {
                        LogEvents(HttpStatusCode.InternalServerError, "Response is null");
                    }
                }
                return false;
            }
        }

        public string ShapePointsToString(routeObj json)
        {

            //Formatted as 58,1759649997499.11.4035799936928;
            var StringOfLatlong = "";
            var latindexer = 0;
            var longindexer = 1;
            do
            {
                StringOfLatlong += json.route.shape.shapePoints[latindexer].ToString() + ".";
                StringOfLatlong += json.route.shape.shapePoints[longindexer].ToString() + ";";
                latindexer = latindexer + 2;
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