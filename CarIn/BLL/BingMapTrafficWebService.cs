using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using CarIn.BLL.Abstract;
using CarIn.DAL.Repositories;
using CarIn.Models.Entities;
using Newtonsoft.Json.Linq;

namespace CarIn.BLL
{
    public class BingMapTrafficWebService : IWebService<TrafficIncident>
    {
        private readonly string _key;
        private readonly string _southLat;
        private readonly string _westLong;
        private readonly string _northLat;
        private readonly string _eastLong;
        
        private List<TrafficIncident> _trafficIncidents = new List<TrafficIncident>();  


         //"http://dev.virtualearth.net/REST/v1/Traffic/Incidents/57.497813,11.602687,57.885356,12.406062?key=AoWk0xixw7Xr16xE6Tne-3nNsYihl9ab7yIhnoASonYm2sWCdYk7VNhhAUg82cUj";
        
        public BingMapTrafficWebService(string bingKey)
            : this(bingKey, 57.497813, 11.602687, 57.885356, 12.406062){}

        public BingMapTrafficWebService(string bingKey, double southLat, double westLong, double northLat, double eastLong)
        {
            _key = bingKey;
            _southLat = southLat.ToString(CultureInfo.InvariantCulture);
            _westLong = westLong.ToString(CultureInfo.InvariantCulture);
            _northLat = northLat.ToString(CultureInfo.InvariantCulture);
            _eastLong = eastLong.ToString(CultureInfo.InvariantCulture);
        }


        public bool MakeRequest()
        {

                var trafficRequestURL =
                    string.Format("http://dev.virtualearth.net/REST/v1/Traffic/Incidents/{0},{1},{2},{3}?key={4}",
                                  _southLat, _westLong, _northLat, _eastLong, _key);

                var request = (HttpWebRequest)WebRequest.Create(trafficRequestURL);

                request.Method = WebRequestMethods.Http.Get;

                request.Accept = "application/json";

            return GetResponse(request);
        }

        public bool GetResponse(HttpWebRequest request)
        {
            try
            {
                JObject jObject;
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    LogEvents(response.StatusCode, response.StatusDescription);

                    using (var sr = new StreamReader(response.GetResponseStream()))
                    {
                        jObject = JObject.Parse(sr.ReadLine());
                    }
                }

                ParseResponse(jObject);
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

        private void ParseResponse(JObject response)
        {

            var totalResources = (int)response["resourceSets"][0]["estimatedTotal"];
            var jArray = (JArray)response["resourceSets"][0]["resources"];

            _trafficIncidents = new List<TrafficIncident>();
            for (var i = 0; i < totalResources; i++)
            {
                var trafficIncident = new TrafficIncident()
                {
                    IncidentId = (string)jArray[i]["incidentId"],
                    Description = (string)jArray[i]["description"],
                    Verified = (string)jArray[i]["verified"],
                    IncidentType = (string)jArray[i]["type"],
                    End = (string)jArray[i]["end"],
                    LastModified = (string)jArray[i]["lastModified"],
                    RoadClosed = (string)jArray[i]["roadClosed"],
                    Severity = (string)jArray[i]["severity"],
                    Lane = (string)jArray[i]["lane"],
                    Start = (string)jArray[i]["start"],
                    PointLat = (string)jArray[i]["point"]["coordinates"][0],
                    PointLong = (string)jArray[i]["point"]["coordinates"][1],
                    ToPointLat = (string)jArray[i]["toPoint"]["coordinates"][0],
                    ToPointLong = (string)jArray[i]["toPoint"]["coordinates"][1]
                };

                _trafficIncidents.Add(trafficIncident);
            }

        }

        public List<TrafficIncident> GetParsedResponse()
        {
            return _trafficIncidents;
        }

        public void LogEvents(HttpStatusCode statusCode, string statusMessage)
        {
            LoggHelper.SetLogg("BingMapTrafficWebService", statusCode.ToString(), statusMessage);         
        }
    }
}