using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml.Linq;
using CarIn.BLL.Abstract;
using CarIn.Models.Entities;

namespace CarIn.BLL
{
    public class VasttrafikTrafficWebService : IWebService<VasttrafikIncident>
    {
        private readonly string _key;

        private List<VasttrafikIncident> _vasttrafikIncidents = new List<VasttrafikIncident>();  

        public VasttrafikTrafficWebService(string vasstrafikKey)
        {
            _key = vasstrafikKey;
        }

        public void MakeRequest()
        {

            var vasttrafikRequestURL =
                string.Format(
                    "http://www.vasttrafik.se//external_services/TrafficInformation.asmx/GetTrafficInformationWithGeography?identifier={0}",
                    _key);

            var request = (HttpWebRequest) WebRequest.Create(vasttrafikRequestURL);
            request.Method = WebRequestMethods.Http.Get;
            request.Accept = "text/xml";
            GetResponse(request);

        }
        public void GetResponse(HttpWebRequest request)
        {
            try
            {
                XElement xElement;
                using (var response = (HttpWebResponse) request.GetResponse())
                {
                    LogEvents(response.StatusCode, response.StatusDescription);

                    using (var sr = new StreamReader(response.GetResponseStream()))
                    {
                        xElement = XElement.Load(sr);
                    }
                }

                ParseResponse(xElement);
            }
            catch (WebException ex)
            {
                if (ex.Status != WebExceptionStatus.ProtocolError)
                {
                    //throw new NotImplementedException();
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
                        throw new NotImplementedException();
                    }
                }
            }
        }

        private void ParseResponse(XElement vasttrafikResponse)
        {
            var trafficInfos = vasttrafikResponse.Elements();
            foreach (var trafficInfo in trafficInfos)
            {
                var trafficNodes = trafficInfo.Elements();
                if (!string.IsNullOrEmpty(trafficNodes.ElementAt(16).Value))
                {
                    _vasttrafikIncidents.Add(new VasttrafikIncident
                    {
                        Title = trafficNodes.ElementAt(0).Value,
                        Line = trafficNodes.ElementAt(3).Value,
                        DateFrom = trafficNodes.ElementAt(4).Value,
                        DateTo = trafficNodes.ElementAt(5).Value,
                        Priority = trafficNodes.ElementAt(6).Value,
                        TrafficChangesCoords =
                        ReplaceDotWithColonToSeparetCoords(trafficNodes.ElementAt(16).Value),

                    });
                }
            }

        }

        public List<VasttrafikIncident> GetParsedResponse()
        {
            return _vasttrafikIncidents;
        }

        public void LogEvents(HttpStatusCode statusCode, string statusMessage)
        {
            
        }



        //example param : "58,1759649997499,11,4035799936928;58,1759649997499,11,4035799936928"
        private string ReplaceDotWithColonToSeparetCoords(string coordsUnformated)
        {
            try
            {

                string coordsObjects = "";
                String[] stringArrayForCoords;

                if (coordsUnformated.Contains(";"))
                {
                    stringArrayForCoords = coordsUnformated.Split(';');
                }
                else
                {
                    stringArrayForCoords = new string[1];
                    stringArrayForCoords[0] = coordsUnformated;
                }

                foreach (var coord in stringArrayForCoords)
                {
                    var counterForColons = 0;
                    var tmpArray = coord.ToCharArray();

                    for (int i = 0; i < tmpArray.Length; i++)
                    {
                        if (tmpArray[i] == ',')
                        {
                            counterForColons++;
                            if (counterForColons == 2)
                            {
                                tmpArray[i] = '.';
                            }
                        }
                    }

                    var coordsString = new string(tmpArray);
                    coordsObjects += new string(tmpArray);
                    coordsObjects += ";";

                }
                return coordsObjects;
            }
            catch (Exception e)
            {
                return "";
            }
        }

    }
}