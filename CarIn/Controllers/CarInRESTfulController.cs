﻿using System;
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

        private readonly ProccessReqFromWebService _proccessReqFromWebService;

        public CarInRESTfulController (IRepository<TrafficIncident> trafficRepository, IRepository<WheatherPeriod> wheaterRepository, IRepository<VasttrafikIncident> vasttrafikRepository, IRepository<MapQuestDirection> directionsRepository, IRepository<TollLocation> tollLocationRepository)
        {
            _proccessReqFromWebService = new ProccessReqFromWebService(trafficRepository, wheaterRepository, vasttrafikRepository, directionsRepository, tollLocationRepository);
        }

        // GET api/v1/carinrestful/GetAllInfo
        public HttpResponseMessage GetAllInfo()
        {
            //TODO Lägga till felhanteringen och skicka med felkoder
            var mapInfoModel = _proccessReqFromWebService.ProccesReqFromParams("all", "all", "all", "all", "all");

            var response = Request.CreateResponse(HttpStatusCode.OK, mapInfoModel);

            return response;
        }

        // GET api/v1/CarInRESTful/GetInfoFromParams?traffic=all&wheather=all&localTraffic=all&directions=all

        public HttpResponseMessage GetInfoFromParams(string traffic, string wheather, string localTraffic, string directions, string tolls)
        {
            var mapInfoModel = _proccessReqFromWebService.ProccesReqFromParams(traffic, wheather, localTraffic, directions, tolls);
            var response = Request.CreateResponse(HttpStatusCode.OK, mapInfoModel);

            return response;
        }

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
