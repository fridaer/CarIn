using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web;
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
        public void BeginTimerFor()
        {
            _timerForBing = new Timer(x => MakeReqForBing(), null, 0, Timeout.Infinite);
        }

        private void MakeReqForBing()
        {
            _bingMapTrafficWebService.MakeRequest();
            
            _timerForBing.Change(5000, Timeout.Infinite);
        }
    }
}

 