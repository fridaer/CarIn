using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using CarIn.BLL.Abstract;
using CarIn.Models.Entities;
using Newtonsoft.Json.Linq;

namespace CarIn.BLL
{
    public class YrWeatherWebService : IWebService<WheatherPeriod>
    {
        private List<WheatherPeriod> _wheatherPeriods = new List<WheatherPeriod>(); 
        public void MakeRequest()
        {

            var yrWheatherRequestURL =
                string.Format("http://www.yr.no/place/Sweden/V%C3%A4stra_G%C3%B6taland/Gothenburg/forecast.xml");

            var request = (HttpWebRequest)WebRequest.Create(yrWheatherRequestURL);
            request.Method = WebRequestMethods.Http.Get;
            request.Accept = "text/xml";
            GetResponse(request);
        }

        public void GetResponse(HttpWebRequest request)
        {
            try
            {
                XDocument xDocument;
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    LogEvents(response.StatusCode, response.StatusDescription);

                    using (var sr = new StreamReader(response.GetResponseStream()))
                    {
                        xDocument = XDocument.Load(sr);
                    }
                }
                ParseRespone(xDocument);
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
            }
        }

        private void ParseRespone(XDocument YrWheatherResponse)
        {
            var periodicalWheather = YrWheatherResponse.Root.Element("forecast")
                .Element("tabular")
                .Elements("time")
                .Take(4);
            foreach (var period in periodicalWheather)
            {
                _wheatherPeriods.Add(new WheatherPeriod
                                        {
                                            PeriodNumber = period.Attribute("period").Value,
                                            SymbolName = period.Element("symbol").Attribute("name").Value,
                                            WindCode = period.Element("windDirection").Attribute("code").Value,
                                            WindSpeedMps = period.Element("windSpeed").Attribute("mps").Value,
                                            TemperatureCelsius = period.Element("temperature").Attribute("value").Value
                                        });
            }
        }

        public List<WheatherPeriod> GetParsedResponse()
        {
            return _wheatherPeriods;
        }

        public void LogEvents(HttpStatusCode statusCode, string statusMessage)
        {
            LoggHelper.SetLogg("YrWheatherWebService", statusCode.ToString(), statusMessage);
        }

    }
}