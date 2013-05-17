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
            var trafficChangesCoords = new List<string>();
            foreach (var trafficInfo in trafficInfos)
            {
                var trafficNodes = trafficInfo.Elements();
               
                vasttrafikTrafficIncidents.Add(new VasttrafikIncident
                                              {
                                                  Title = trafficNodes.ElementAt(0).Value,
                                                  Line = trafficNodes.ElementAt(3).Value,
                                                  DateFrom = trafficNodes.ElementAt(4).Value,
                                                  DateTo = trafficNodes.ElementAt(5).Value,
                                                  Priority = trafficNodes.ElementAt(6).Value                                                  
                                                   
                                              });
                var tmpTraffiCoords = trafficNodes.ElementAt(16).Value;
                if(!string.IsNullOrEmpty(tmpTraffiCoords))
                {
                    trafficChangesCoords.Add(tmpTraffiCoords);                    
                }
            }
            try
            {
                var latLongObjectsList = new List<object>();

                foreach (var trafficChangesCoord in trafficChangesCoords)
                {

                    latLongObjectsList.Add(new
                                        {
                                            latLongObject = SplitStringIntoLatLong(trafficChangesCoord)
                                        });
                }
                var tmp = latLongObjectsList;
            }

            catch(Exception e)
            {
                
            }


            return vasttrafikTrafficIncidents;
        }


        //58,1759649997499,11,4035799936928;58,1759649997499,11,4035799936928
        private IEnumerable<object> SplitStringIntoLatLong(string p)
        {
            try
            {

            
            List<object> coordsObjects = new List<object>();
            String[] stringArrayForCoords;

            if(p.Contains(";"))
            {
                stringArrayForCoords = p.Split(';');
            }
            else
            {
                stringArrayForCoords = new string[1];
                stringArrayForCoords[0] = p;
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
                var latLongArray = coordsString.Split('.');


                coordsObjects.Add(new
                                {
                                    latitude = latLongArray[0],
                                    longitude = latLongArray[1]
                                });
        
            }
            return coordsObjects;
            }
            catch (Exception e)
            {
                var tmp = e;
                return new List<object>();
            }
            //var toSplitUpToCoordsObjects = p.ToCharArray();
            //int counterForColons = 0, counterForSemiColons = 0, lastIndexForSplittingString = 0;
            //var latLongObjects = new List<object>();
            //for (int i = 0; i < toSplitUpToCoordsObjects.Length; i++)
            //{
            //    switch (toSplitUpToCoordsObjects[i])
            //    {
            //        case ';':
            //            counterForColons--;
            //            counterForSemiColons++;
            //                latLongObjects.Add(new { coordinates = new string(toSplitUpToCoordsObjects, lastIndexForSplittingString, i) });
            //                lastIndexForSplittingString = i;

            //            break;
            //        case ',':
            //            counterForColons++;
            //            if(counterForColons % 2 == 0)
            //            {
            //                toSplitUpToCoordsObjects[i] = '.';
            //            }
            //            break;
            //    }
            //}

            //var tmp2 = latLongObjects.ToList();
            //var manipulatedString = new string(toSplitUpToCoordsObjects);

            //for (int i = 0; i < counterForSemiColons; i++)
            //{
            //    foreach (var character in manipulatedString)
            //    {
            //        if(character == ';')
            //        {

            //        }
            //    //latLongObjects.Add(new object{ lat =  });
                           
            //    }
            //}
            //var tmp = new string(toSplitUpToCoordsObjects);
        }
    }
}