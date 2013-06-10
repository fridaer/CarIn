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

        static IRepository<TrafficIncident> _trafficRespository = new Repository<TrafficIncident>();
        static IRepository<WheatherPeriod> _wheatherRespository = new Repository<WheatherPeriod>();
        static IRepository<VasttrafikIncident> _vasttrafikRespository = new Repository<VasttrafikIncident>();
        static IRepository<MapQuestDirection> _directionsRepository = new Repository<MapQuestDirection>();

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

        private Mutex _mutext;

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

            _mutext = new Mutex();
        }
        public void BeginTimers()
        {
            _timerForBing = new Timer(x => MakeReqForBing(), null, 0, Timeout.Infinite);
            _timerForYr = new Timer(x => MakeReqForYr(), null, 0, Timeout.Infinite);
            _timerForVasttrafik = new Timer(x => MakeReqForVasttrafik(), null, 0, Timeout.Infinite);
        }

        private void MakeReqForBing()
        {
            if(_bingMapTrafficWebService.MakeRequest())
            {
                var trafficIncidents = _bingMapTrafficWebService.GetParsedResponse().ToList();
                _trafficRespository.TruncateTable("TrafficIncidents");

                foreach (var trafficIncident in trafficIncidents)
                {
                    _trafficRespository.AddForBulk(trafficIncident);
                }
                _mutext.WaitOne();

                _trafficRespository.Commit();

                _mutext.ReleaseMutex();

                _eventLogger.WriteEntry(string.Format("{0} updated", "BingDb"));

                if (!_isDown)
                {
                    _mapQuestDirectionsWebService.TakeTrafficIncident(trafficIncidents);

                    if (_mapQuestDirectionsWebService.MakeRequest())
                    {
                        var mapQuestDirections = _mapQuestDirectionsWebService.GetParsedResponse().ToList();
                        
                        _directionsRepository.TruncateTable("MapQuestDirections");

                        foreach (var mapQuestDirection in mapQuestDirections)
                        {
                            _directionsRepository.AddForBulk(mapQuestDirection);
                        }
                        _mutext.WaitOne();

                        _directionsRepository.Commit();

                        _mutext.ReleaseMutex();
                        
                        _eventLogger.WriteEntry(string.Format("{0} updated", "MapQuestDb"));
                        _directionsRepository.Dispose();
                        Process currentProcess = System.Diagnostics.Process.GetCurrentProcess();
                        _eventLogger.WriteEntry(currentProcess.WorkingSet64 + " TotalMemory used bing");
                        _directionsRepository = new Repository<MapQuestDirection>();

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

                _timerForBing.Change(300000, Timeout.Infinite);
                _eventLogger.WriteEntry(string.Format("{0} called starting over with 5min", "bing"));
                _trafficRespository.Dispose();
                Process _currentProcess = System.Diagnostics.Process.GetCurrentProcess();
                long totalBytesOfMemoryUsed = _currentProcess.WorkingSet64;
                _eventLogger.WriteEntry(totalBytesOfMemoryUsed + " TotalMemory used bing");
                _trafficRespository = new Repository<TrafficIncident>();
                
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
                var wheatherPeriods = _yrWeatherWebService.GetParsedResponse().ToList();
                _wheatherRespository.TruncateTable("WheatherPeriods");

                foreach (var wheatherPeriod in wheatherPeriods)
                {
                    _wheatherRespository.AddForBulk(wheatherPeriod);
                }

                _mutext.WaitOne();

                _wheatherRespository.Commit();

                _mutext.ReleaseMutex();


                _timerForYr.Change(120000, Timeout.Infinite);
                _eventLogger.WriteEntry(string.Format("{0} called starting over with 20min", "Yr"));
                _wheatherRespository.Dispose();
                Process currentProcess = System.Diagnostics.Process.GetCurrentProcess();

                long totalBytesOfMemoryUsed = currentProcess.WorkingSet64;
                _eventLogger.WriteEntry(totalBytesOfMemoryUsed + " TotalMemory used yr");
                _wheatherRespository = new Repository<WheatherPeriod>();
            }
            else
            {

                if (_errorCounterYr <= 5)
                {
                    _timerForYr.Change(10000, Timeout.Infinite);
                    _eventLogger.WriteEntry(string.Format("{0} called starting over with 10sec", "Yr"));
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
                var vasttrafikTrafficIncidents = _vasttrafikTrafficWebService.GetParsedResponse().ToList();
                _vasttrafikRespository.TruncateTable("VasttrafikIncidents");

                foreach (var vasttrafikTrafficIncident in vasttrafikTrafficIncidents)
                {
                    _vasttrafikRespository.AddForBulk(vasttrafikTrafficIncident);
                }

                _mutext.WaitOne();

                _vasttrafikRespository.Commit();

                _mutext.ReleaseMutex();

                _timerForVasttrafik.Change(600000, Timeout.Infinite);
                _eventLogger.WriteEntry(string.Format("{0} called starting over with 10min", "Vasstrafik"));
                _vasttrafikRespository.Dispose();
                Process currentProcess = System.Diagnostics.Process.GetCurrentProcess();

                long totalBytesOfMemoryUsed = currentProcess.WorkingSet64;
                _eventLogger.WriteEntry(totalBytesOfMemoryUsed + " TotalMemory used vast");
                _vasttrafikRespository = new Repository<VasttrafikIncident>();
            }
            else
            {
                if (_errorCounterVasttrafik <= 5)
                {
                    _timerForVasttrafik.Change(10000, Timeout.Infinite);
                    _eventLogger.WriteEntry(string.Format("{0} called starting over with 10sec", "Vasttrafik"));
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
            _trafficRespository.Dispose();
            _directionsRepository.Dispose();
            _wheatherRespository.Dispose();
            _vasttrafikRespository.Dispose();
        }
      
    }
}

 