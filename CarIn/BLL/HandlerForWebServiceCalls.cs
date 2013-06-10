using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web;
using CarIn.BLL.Abstract;
using CarIn.DAL.Repositories;
using CarIn.DAL.Repositories.Abstract;
using CarIn.Models.Entities;
using SUI.Helpers;

namespace CarIn.BLL
{
    public class HandlerForWebServiceCalls
    {
        private BingMapTrafficWebService _bingMapTrafficWebService;
        private MapQuestDirectionsWebService _mapQuestDirectionsWebService;
        private YrWeatherWebService _yrWeatherWebService;
        private VasttrafikTrafficWebService _vasttrafikTrafficWebService;

        static readonly IRepository<TrafficIncident> _trafficRespository = new Repository<TrafficIncident>();
        static readonly IRepository<WheatherPeriod> _wheatherRespository = new Repository<WheatherPeriod>();
        static readonly IRepository<VasttrafikIncident> _vasttrafikRespository = new Repository<VasttrafikIncident>();
        static readonly IRepository<MapQuestDirection> _directionsRepository = new Repository<MapQuestDirection>();

        private Timer _timerForBing;
        private Timer _timerForYr;
        private Timer _timerForVasttrafik;

        private EventLog _eventLogger;

        private int _errorCounterBing;
        private int _errorCounterYr;
        private int _errorCounterVasttrafik;
        private int _errorCounterMapQuest;

        private readonly int _hour;

        private bool _isDown;

        public HandlerForWebServiceCalls(EventLog eventLogger)
        {
            _eventLogger = eventLogger;
 
            _bingMapTrafficWebService = new BingMapTrafficWebService("AoWk0xixw7Xr16xE6Tne-3nNsYihl9ab7yIhnoASonYm2sWCdYk7VNhhAUg82cUj");
            _mapQuestDirectionsWebService = new MapQuestDirectionsWebService();
            _yrWeatherWebService = new YrWeatherWebService();
            _vasttrafikTrafficWebService = new VasttrafikTrafficWebService("key");

            _errorCounterBing = 0;
            _errorCounterYr = 0;
            _errorCounterVasttrafik = 0;
            _errorCounterMapQuest = 0;
            _isDown = false;
            _hour = 60000 * 60;

        }
        public void BeginTimers()
        {
            _timerForBing = new Timer(x => MakeReqForBing(), null, 0, Timeout.Infinite);
            //_timerForYr = new Timer(x => MakeReqForYr(), null, 0, Timeout.Infinite);
            //_timerForVasttrafik = new Timer(x => MakeReqForVasttrafik(), null, 0, Timeout.Infinite);
        }

        private void MakeReqForBing()
        {
            if(_bingMapTrafficWebService.MakeRequest())
            {
                var trafficIncidents = _bingMapTrafficWebService.GetParsedResponse().ToList();
                _trafficRespository.TruncateTable("TrafficIncidents");

                foreach (var trafficIncident in trafficIncidents)
                {
                    _trafficRespository.Add(trafficIncident);
                }

                _eventLogger.WriteEntry(string.Format("{0} updated", "BingDb"));

                if (!_isDown)
                {
                    _mapQuestDirectionsWebService.TakeTrafficIncident(trafficIncidents);

                    if (_mapQuestDirectionsWebService.MakeRequest())
                    {
                        var mapQuestDirections = _mapQuestDirectionsWebService.GetParsedResponse();
                        _directionsRepository.TruncateTable("MapQuestDirections");
                        mapQuestDirections.ForEach(x => _directionsRepository.Add(x));
                        _eventLogger.WriteEntry(string.Format("{0} updated", "MapQuestDb"));

                    }
                    else
                    {
                        if (_errorCounterMapQuest <= 5)
                        {
                            _errorCounterMapQuest++;
                            _eventLogger.WriteEntry(string.Format("{0} error", "MapQuestDb"));
                        }
                        else
                        {
                            _isDown = true;
                            _eventLogger.WriteEntry(string.Format("{0} Errorcounter reached 5", "MapQuest"));
                            MailHelper.SendEmail("MapQuest", "Error calling mapQuest 5 times", DateTime.Now);

                        }
                    }
                }

                _timerForBing.Change(30000, Timeout.Infinite);
                _eventLogger.WriteEntry(string.Format("{0} called starting over with 30sec", "bing"));
                
            }
            else
            {
                if (_errorCounterBing <= 5)
                {
                    _timerForBing.Change(30000, Timeout.Infinite);
                    _eventLogger.WriteEntry(String.Format("{0} called starting over with 30sec", "bing"));
                    _errorCounterBing++;
                }
                else
                {
                    _errorCounterBing = 0;

                    _timerForBing.Change(_hour*6, Timeout.Infinite);
                    _eventLogger.WriteEntry(string.Format("{0} Errorcounter reached 5", "Bing"));
                    
                    MailHelper.SendEmail("BingService", "Error calling bing 5 times sleep for 6h", DateTime.Now);
                }
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

                if (_errorCounterYr <= 5)
                {
                    _timerForYr.Change(1000, Timeout.Infinite);
                    _eventLogger.WriteEntry(string.Format("{0} called starting over with 1sec", "Yr"));
                    _errorCounterYr++;
                }
                else
                {
                    _errorCounterYr = 0;

                    _timerForYr.Change(_hour * 6, Timeout.Infinite);
                    _eventLogger.WriteEntry(string.Format("{0} Errorcounter reached 5", "Yr"));
                    MailHelper.SendEmail("YrService", "Error calling Yr 5 times sleep for 6h", DateTime.Now);
                }
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
                if (_errorCounterVasttrafik <= 5)
                {
                    _timerForVasttrafik.Change(1000, Timeout.Infinite);
                    _eventLogger.WriteEntry(string.Format("{0} called starting over with 1sec", "Vasttrafik"));
                    _errorCounterVasttrafik++;
                }
                else
                {
                    _errorCounterVasttrafik = 0;
                    _timerForVasttrafik.Change(_hour * 6, Timeout.Infinite);
                    _eventLogger.WriteEntry(string.Format("{0} Errorcounter reached 5", "Vasttrafik"));
                    MailHelper.SendEmail("VasttrafikService", "Error calling Vasttrafik 5 times sleep for 6h", DateTime.Now);
                
                }
            }
        }
        public void StopTimers()
        {
            _isDown = false;
            _timerForBing.Dispose();
            _timerForYr.Dispose();
            _timerForVasttrafik.Dispose();
        }
    }
}

 