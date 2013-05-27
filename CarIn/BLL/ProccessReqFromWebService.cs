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


        public ProccessReqFromWebService(IRepository<TrafficIncident> trafficRepository, IRepository<WheatherPeriod> wheaterRepository, IRepository<VasttrafikIncident> vasttrafikRepository, IRepository<MapQuestDirection> mapQuestRepository)
        {
            _trafficRepository = trafficRepository;
            _wheaterRepository = wheaterRepository;
            _vasttrafikRepository = vasttrafikRepository;
            _mapQuestRepository = mapQuestRepository;
        }

        public MapInfoVm ProccesReqFromParams(string traffic, string wheather, string localTraffic, string mapQuest)
        {
            var mapInfoModel = new MapInfoVm();

            if (string.IsNullOrWhiteSpace(traffic))
            {
                mapInfoModel.TrafficIncidents = null;
            }
            else
            {
                switch (traffic.ToLower())
                {
                    case "all":
                        mapInfoModel.TrafficIncidents = _trafficRepository.FindAll().ToList();
                        break;
                    case "serious":
                        mapInfoModel.TrafficIncidents = _trafficRepository.FindAll(x => int.Parse(x.Severity) == 4).ToList();
                        break;
                    default:
                        mapInfoModel.TrafficIncidents = null;
                        break;
                }
            }


            if (string.IsNullOrWhiteSpace(wheather))
            {
                mapInfoModel.WheatherPeriods = null;
            }
            else
            {
                mapInfoModel.WheatherPeriods = wheather.ToLower() == "all" ? _wheaterRepository.FindAll().ToList() : null;
            }


            if (string.IsNullOrWhiteSpace(localTraffic))
            {
                mapInfoModel.VasttrafikIncidents = null;
            }
            else
            {
                if (localTraffic.ToLower() == "all")
                {
                    mapInfoModel.VasttrafikIncidents = _vasttrafikRepository.FindAll().ToList();
                }
                else if (localTraffic.ToLower() == "serious")
                {
                    mapInfoModel.VasttrafikIncidents = _vasttrafikRepository.FindAll(x => int.Parse(x.Priority) == 1).ToList();
                }
                else
                {
                    mapInfoModel.VasttrafikIncidents = null;
                }
            }
            if (string.IsNullOrWhiteSpace(mapQuest))
            {
                mapInfoModel.MapQuestDirections = null;
            }
            else
            {
                if (localTraffic.ToLower() == "all")
                {
                    mapInfoModel.MapQuestDirections = _mapQuestRepository.FindAll().ToList();
                }
                else
                {
                    mapInfoModel.MapQuestDirections = null;
                }
            }
            return mapInfoModel;
        }
    }
}