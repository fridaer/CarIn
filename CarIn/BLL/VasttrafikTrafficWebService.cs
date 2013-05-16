using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml.Linq;
using CarIn.Models.Entities;

namespace CarIn.BLL
{
    public class VasttrafikTrafficWebService
    {

        public List<VasttrafikIncident> MakeRequest()
        {
            try
            {
                var vasttrafikRequestURL =
                    string.Format(
                        "http://www.vasttrafik.se//external_services/TrafficInformation.asmx/GetTrafficInformationWithGeography?identifier={0}",
                        "string");

                var request = (HttpWebRequest) WebRequest.Create(vasttrafikRequestURL);
                request.Method = WebRequestMethods.Http.Get;
                request.Accept = "text/xml";

                XElement xElement;
                using (var response = (HttpWebResponse) request.GetResponse())
                {
                    using (var sr = new StreamReader(response.GetResponseStream()))
                    {
                        xElement = XElement.Load(sr);
                    }
                }
                var vasttrafikIncidents = GetResponse(xElement);

                return vasttrafikIncidents;
            }
            catch (Exception e)
            {
                return new List<VasttrafikIncident>();
            }
        }

        private List<VasttrafikIncident> GetResponse(XElement vasttrafikResponse)
        {
            var trafficInfos = vasttrafikResponse.Elements();
            var vasttrafikTrafficIncidents = new List<VasttrafikIncident>();
            foreach (var trafficInfo in trafficInfos)
            {
                var trafficNodes = trafficInfo.Elements();
               
                vasttrafikTrafficIncidents.Add(new VasttrafikIncident
                                              {
                                                  Title = trafficNodes.ElementAt(0).Value,
                                                  Line = trafficNodes.ElementAt(3).Value,
                                                  DateFrom = trafficNodes.ElementAt(4).Value,
                                                  DateTo = trafficNodes.ElementAt(5).Value,
                                                  Priority = trafficNodes.ElementAt(6).Value,
                                                  TrafficChangesCoords = trafficNodes.ElementAt(16).Value
                                              });
            }
            return vasttrafikTrafficIncidents;
        }
    }
}