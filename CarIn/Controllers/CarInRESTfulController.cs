using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;
using CarIn.BLL;
using CarIn.DAL.Repositories;
using CarIn.DAL.Repositories.Abstract;
using CarIn.Models.Entities;
using CarIn.Models.ViewModels;
using Newtonsoft.Json.Linq;

namespace CarIn.Controllers
{
    public class CarInRESTfulController : ApiController
    {
        //private readonly IRepository<TrafficIncident> _trafficRepository;
        //private readonly IRepository<WheatherPeriod> _wheaterRepository;
        //private readonly IRepository<VasttrafikIncident> _vasttrafikRepository;
        private ProccessReqFromWebService _proccessReqFromWebService;

        public CarInRESTfulController(IRepository<TrafficIncident> trafficRepository, IRepository<WheatherPeriod> wheaterRepository, IRepository<VasttrafikIncident> vasttrafikRepository)
        {
            _proccessReqFromWebService = new ProccessReqFromWebService(trafficRepository, wheaterRepository, vasttrafikRepository);
            //_trafficRepository = trafficRepository;
            //_wheaterRepository = wheaterRepository;
            //_vasttrafikRepository = vasttrafikRepository;
        }

        // GET api/v1/carinrestful/GetAllInfo
        public HttpResponseMessage GetAllInfo()
        {
            //var mapInfoModel = new MapInfoVm
            //{
            //    TrafficIncidents = _trafficRepository.FindAll().ToList(),
            //    WheatherPeriods = _wheaterRepository.FindAll().ToList(),
            //    VasttrafikIncidents = _vasttrafikRepository.FindAll().ToList()
            //};

            var mapInfoModel = _proccessReqFromWebService.ProccesReqFromParams("all", "all", "all");
            var response = Request.CreateResponse(HttpStatusCode.OK, mapInfoModel);
            return response;
        }

        // GET api/v1/CarInRESTful/GetInfoFromParams?traffic=all&wheather=all&localTraffic=all

        public HttpResponseMessage GetInfoFromParams(string traffic, string wheather, string localTraffic)
        {
            var mapInfoModel = _proccessReqFromWebService.ProccesReqFromParams(traffic, wheather, localTraffic);

            var response = Request.CreateResponse(HttpStatusCode.OK, mapInfoModel);

            return response;
        }

        //private MapInfoVm ProccesReqFromParams(string traffic, string wheather, string localTraffic)
        //{
        //    var mapInfoModel = new MapInfoVm();

        //    if (string.IsNullOrWhiteSpace(traffic))
        //    {
        //        mapInfoModel.TrafficIncidents = null;
        //    }
        //    else
        //    {
        //        switch (traffic.ToLower())
        //        {
        //            case "all":
        //                mapInfoModel.TrafficIncidents = _trafficRepository.FindAll().ToList();
        //                break;
        //            case "serious":
        //                mapInfoModel.TrafficIncidents = _trafficRepository.FindAll(x => int.Parse(x.Severity) == 4).ToList();
        //                break;
        //            default:
        //                mapInfoModel.TrafficIncidents = null;
        //                break;
        //        }
        //    }


        //    if (string.IsNullOrWhiteSpace(wheather))
        //    {
        //        mapInfoModel.WheatherPeriods = null;
        //    }
        //    else
        //    {
        //        mapInfoModel.WheatherPeriods = wheather.ToLower() == "all" ? _wheaterRepository.FindAll().ToList() : null;
        //    }


        //    if (string.IsNullOrWhiteSpace(localTraffic))
        //    {
        //        mapInfoModel.VasttrafikIncidents = null;
        //    }
        //    else
        //    {
        //        if (localTraffic.ToLower() == "all")
        //        {
        //            mapInfoModel.VasttrafikIncidents = _vasttrafikRepository.FindAll().ToList();
        //        }
        //        else if (localTraffic.ToLower() == "serious")
        //        {
        //            mapInfoModel.VasttrafikIncidents = _vasttrafikRepository.FindAll(x => int.Parse(x.Priority) == 1).ToList();
        //        }
        //        else
        //        {
        //            mapInfoModel.VasttrafikIncidents = null;
        //        }
        //    }
        //    return mapInfoModel;
        //}


        // POST api/v1/carinrestful
        public void Post([FromBody]string value)
        {
        }

        // PUT api/v1/carinrestful/5
        public void Put(int id, [FromBody]string value)
        {

        }

        // DELETE api/v1/carinrestful/5
        public void Delete(int id)
        {

        }
    }
}