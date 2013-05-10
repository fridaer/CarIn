using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using CarIn.Models.Entities;
using Newtonsoft.Json.Linq;

namespace CarIn.BLL
{
    public class YrWeatherWebService
    {
        public List<WheatherPeriod> MakeRequest()
        {
            try
            {
                var yrWheatherRequestURL =
                    string.Format("http://www.yr.no/place/Sweden/V%C3%A4stra_G%C3%B6taland/Gothenburg/forecast.xml");

                var request = (HttpWebRequest)WebRequest.Create(yrWheatherRequestURL);
                request.Method = WebRequestMethods.Http.Get;
                request.Accept = "text/xml";

                XDocument xDocument;
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    using (var sr = new StreamReader(response.GetResponseStream()))
                    {                      
                        xDocument = XDocument.Load(sr);
                    }
                }

                return GetResponse(xDocument);
            }
            catch (WebException ex)
            {

                return new List<WheatherPeriod>();
            }
            catch (Exception e)
            {
                return new List<WheatherPeriod>();
            }
        }
        private List<WheatherPeriod> GetResponse(XDocument YrWheatherResponse)
        {
           var periodicalWheather = YrWheatherResponse.Root.Element("forecast")
                                                          .Element("tabular")
                                                          .Elements("time")
                                                          .Take(4);
            var wheatherPeriods = new List<WheatherPeriod>();
            foreach (var period in periodicalWheather)
            {
                wheatherPeriods.Add(new WheatherPeriod
                                        {
                                            PeriodNumber = period.Attribute("period").Value,
                                            SymbolName = period.Element("symbol").Attribute("name").Value,
                                            WindCode = period.Element("windDirection").Attribute("code").Value,
                                            WindSpeedMps = period.Element("windSpeed").Attribute("mps").Value,
                                            TemperatureCelsius = period.Element("temperature").Attribute("value").Value
                                        });
            }
            return wheatherPeriods;
        }
    }
}