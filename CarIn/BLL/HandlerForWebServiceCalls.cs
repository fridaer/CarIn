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

        private EventLog _eventLogger;

        public HandlerForWebServiceCalls()
        {
            _bingMapTrafficWebService = new BingMapTrafficWebService("AoWk0xixw7Xr16xE6Tne-3nNsYihl9ab7yIhnoASonYm2sWCdYk7VNhhAUg82cUj");
            _mapQuestDirectionsWebService = new MapQuestDirectionsWebService();
            _yrWeatherWebService = new YrWeatherWebService();
            _vasttrafikTrafficWebService = new VasttrafikTrafficWebService("key");
        }
        public void BeginTimers(EventLog eventLogger)
        {
            _eventLogger = eventLogger;
            _timerForBing = new Timer(x => MakeReqForBing(), null, 0, Timeout.Infinite);
            _timerForYr = new Timer(x => MakeReqForYr(), null, 0, Timeout.Infinite);
            _timerForVasttrafik = new Timer(x => MakeReqForVasttrafik(), null, 0, Timeout.Infinite);
        }
        //TODO Lägga in en felhantering som kollar antalet fel requests
        private void MakeReqForBing()
        {
            if(_bingMapTrafficWebService.MakeRequest())
            {

                _mapQuestDirectionsWebService.TakeTrafficIncident(_bingMapTrafficWebService.GetParsedResponse().ToList());

                _timerForBing.Change(10000, Timeout.Infinite);
                _eventLogger.WriteEntry(string.Format("{0} called starting over with 10sec", "bing"));
            }
            else
            {
                _timerForBing.Change(1000, Timeout.Infinite);
                _eventLogger.WriteEntry(String.Format("{0} called starting over with 1sec", "bing"));

            }
        }

        private void MakeReqForYr()
        {
            if (_yrWeatherWebService.MakeRequest())
            {
                _timerForYr.Change(20000, Timeout.Infinite);
                _eventLogger.WriteEntry(string.Format("{0} called starting over with 20sec", "Yr"));

            }
            else
            {
                _timerForYr.Change(1000, Timeout.Infinite);
                _eventLogger.WriteEntry(string.Format("{0} called starting over with 1sec", "Yr"));

            }
        }

        private void MakeReqForVasttrafik()
        {
            if (_vasttrafikTrafficWebService.MakeRequest())
            {
                _timerForVasttrafik.Change(25000, Timeout.Infinite);
                _eventLogger.WriteEntry(string.Format("{0} called starting over with 25sec", "Vasstrafik"));

            }
            else
            {
                _timerForVasttrafik.Change(1000, Timeout.Infinite);
                _eventLogger.WriteEntry(string.Format("{0} called starting over with 1sec", "Vasttrafik"));

            }
        }
        public void StopTimers()
        {
            _timerForBing.Dispose();
            _timerForYr.Dispose();
            _timerForVasttrafik.Dispose();
        }
    }
}

 