using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web;
using CarIn.BLL.Abstract;
using CarIn.Models.Entities;

namespace CarIn.BLL
{
    public class HandlerForWebServiceCalls
    {
        private BingMapTrafficWebService _bingMapTrafficWebService;
        private MapQuestDirectionsWebService _mapQuestDirectionsWebService;
        private YrWeatherWebService _yrWeatherWebService;
        private VasttrafikTrafficWebService _vasttrafikTrafficWebService;

        private Timer _timerForBing;
        private Timer _timerForYr;
        private Timer _timerForVasttrafik;

        public HandlerForWebServiceCalls()
        {
            _bingMapTrafficWebService = new BingMapTrafficWebService("AoWk0xixw7Xr16xE6Tne-3nNsYihl9ab7yIhnoASonYm2sWCdYk7VNhhAUg82cUj");
            _mapQuestDirectionsWebService = new MapQuestDirectionsWebService();
            _yrWeatherWebService = new YrWeatherWebService();
            _vasttrafikTrafficWebService = new VasttrafikTrafficWebService("key");
        }
        public void BeginTimers()
        {
            _timerForBing = new Timer(x => MakeReqForBing(), null, 500, Timeout.Infinite);
            _timerForYr = new Timer(x => MakeReqForYr(), null, 1000, Timeout.Infinite);
            _timerForVasttrafik = new Timer(x => MakeReqForVasttrafik(), null, 2000, Timeout.Infinite);
        }
        //TODO Om contextet redan används så kommer det uppstå ett exception, finns möjligheter till krockar här.
        private void MakeReqForBing()
        {
            if(_bingMapTrafficWebService.MakeRequest())
            {

                _mapQuestDirectionsWebService.TakeTrafficIncident(_bingMapTrafficWebService.GetParsedResponse().ToList());
                var result = _mapQuestDirectionsWebService.GetParsedResponse();

                _timerForBing.Change(5000, Timeout.Infinite);
                Debug.WriteLine(string.Format("{0} called starting over with 5sec", "bing"));
            }
            else
            {
                _timerForBing.Change(0, Timeout.Infinite);
                Debug.WriteLine(string.Format("{0} called starting over with 0sec", "bing"));

            }
        }

        private void MakeReqForYr()
        {
            if (_yrWeatherWebService.MakeRequest())
            {
                _timerForYr.Change(10000, Timeout.Infinite);
                Debug.WriteLine(string.Format("{0} called starting over with 10sec", "Yr"));

            }
            else
            {
                _timerForYr.Change(0, Timeout.Infinite);
                Debug.WriteLine(string.Format("{0} called starting over with 0sec", "Yr"));

            }
        }

        private void MakeReqForVasttrafik()
        {
            if (_vasttrafikTrafficWebService.MakeRequest())
            {
                _timerForVasttrafik.Change(22000, Timeout.Infinite);
                Debug.WriteLine(string.Format("{0} called starting over with 22sec", "Vasstrafik"));

            }
            else
            {
                _timerForVasttrafik.Change(0, Timeout.Infinite);
                Debug.WriteLine(string.Format("{0} called starting over with 0sec", "Vasttrafik"));

            }
        }
    }
}

 