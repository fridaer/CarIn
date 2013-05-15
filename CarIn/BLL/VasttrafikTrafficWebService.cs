using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml.Linq;

namespace CarIn.BLL
{
    public class VasttrafikTrafficWebService
    {

        public void MakeRequest()
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
                GetResponse(xElement);
            }
            catch (Exception e)
            {

            }
        }

        private List<object> GetResponse(XElement vasttrafikResponse)
        {
            var trafficInfos = vasttrafikResponse.Elements();
            var trafficInfosNodes = vasttrafikResponse.Nodes();
            //var trafficInfos =
            //    vasttrafikResponse.Element("ArrayOfTrafficInformation").Elements("TrafficInformation");
            var vasttrafikTafficInfos = new List<object>();
            foreach (var trafficInfo in trafficInfos)
            {
                var trafficNodes = trafficInfo.Elements();
                vasttrafikTafficInfos.Add(new
                                              {
                                                  Title = trafficNodes.ElementAt(0).Value,

                                                  Line = trafficInfo.Element("Line").Value,
                                                  DateFrom = trafficInfo.Element("DateFrom").Value,
                                                  DateTo = trafficInfo.Element("DateTo").Value,
                                                  Priority = trafficInfo.Element("Priority").Value,
                                                  TrafficChangesCoords = trafficInfo.Element("TrafficChangesCoords")
                                              });
            }
            return vasttrafikTafficInfos;
        }
    }
}