using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarIn.DAL.Repositories.Abstract;
using CarIn.Models.Entities;
using CarIn.Models.ViewModels;

namespace CarIn.BLL
{
    public class ProccessReqFromWebService
    {
        private readonly IRepository<TrafficIncident> _trafficRepository;
        private readonly IRepository<WheatherPeriod> _wheaterRepository;
        private readonly IRepository<VasttrafikIncident> _vasttrafikRepository;
        private readonly IRepository<MapQuestDirection> _mapQuestRepository;
        private readonly IRepository<TollLocation> _tollLocationRepository;


        public ProccessReqFromWebService(IRepository<TrafficIncident> trafficRepository, IRepository<WheatherPeriod> wheaterRepository,
                                         IRepository<VasttrafikIncident> vasttrafikRepository, IRepository<MapQuestDirection> mapQuestRepository, IRepository<TollLocation> tollLocationRepository)
        {
            _trafficRepository = trafficRepository;
            _wheaterRepository = wheaterRepository;
            _vasttrafikRepository = vasttrafikRepository;
            _mapQuestRepository = mapQuestRepository;
            _tollLocationRepository = tollLocationRepository;
        }

        public MapInfoVm ProccesReqFromParams(string traffic, string wheather, string localTraffic, string mapQuest, string tolls)
        {
            var mapInfoModel = new MapInfoVm
                                   {
                                       TrafficIncidents = GetTrafficInfo(traffic),
                                       WheatherPeriods = GetWheatherInfo(wheather),
                                       VasttrafikIncidents = GetLocalTraffic(localTraffic),
                                       MapQuestDirections = GetMapQuestDirection(mapQuest),
                                       TollLocations = GetTolls(tolls)
                                   };

            
            return mapInfoModel;
        }

        private List<TrafficIncident> GetTrafficInfo(string traffic)
        {
            if (string.IsNullOrWhiteSpace(traffic))
            {
                return null;
            }
            switch (traffic.ToLower())
            {
                case "all":
                    return _trafficRepository.FindAll().ToList();
                case "serious":
                    return _trafficRepository.FindAll(x => int.Parse(x.Severity) == 4).ToList();
                default:
                    return null;
            }
        }
        private List<WheatherPeriod> GetWheatherInfo(string wheather)
        {
            if (string.IsNullOrWhiteSpace(wheather))
            {
                return null;
            }
            switch (wheather.ToLower())
            {
                case "all":
                    return _wheaterRepository.FindAll().ToList();
                default:
                    return null;
            }
        }
        private List<VasttrafikIncident> GetLocalTraffic(string localTraffic)
        {
            if (string.IsNullOrWhiteSpace(localTraffic))
            {
                return null;
            }
            switch (localTraffic.ToLower())
            {
                case "all":
                    return _vasttrafikRepository.FindAll().ToList();
                case "serious":
                    return _vasttrafikRepository.FindAll(x => int.Parse(x.Priority) == 1).ToList();
                default:
                    return null;
            }
        }
        private List<MapQuestDirection> GetMapQuestDirection(string mapQuest)
        {
            if (string.IsNullOrWhiteSpace(mapQuest))
            {
                return null;
            }
            switch (mapQuest.ToLower())
            {
                case "all":
                    return _mapQuestRepository.FindAll().ToList();
                default:
                    return null;
            }
        }
        private List<TollLocation> GetTolls(string tolls)
        {
            if (string.IsNullOrWhiteSpace(tolls))
            {
                return null;
            }
            switch (tolls.ToLower())
            {
                case "all":
                    return _tollLocationRepository.FindAll().ToList();
                default:
                    return null;
            }
        }
        
    }
}