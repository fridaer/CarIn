using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;
using CarIn.DAL.Repositories;
using CarIn.DAL.Repositories.Abstract;
using CarIn.Models.Entities;
using CarIn.Models.ViewModels;
using Newtonsoft.Json.Linq;

namespace CarIn.Controllers
{
    public class CarInRESTfulController : ApiController
    {
        private readonly IRepository<TrafficIncident> _trafficRepository;
        private readonly IRepository<WheatherPeriod> _wheaterRepository;
        private readonly IRepository<VasttrafikIncident> _vasttrafikRepository;

        public CarInRESTfulController(IRepository<TrafficIncident> trafficRepository, IRepository<WheatherPeriod> wheaterRepository, IRepository<VasttrafikIncident> vasttrafikRepository)
        {
            _trafficRepository = trafficRepository;
            _wheaterRepository = wheaterRepository;
            _vasttrafikRepository = vasttrafikRepository;
        }
        public Object GetAllInfo()
        {
            //var trafficIncidents = _repository.FindAll();
            //return trafficIncidents;

            var tmpRepWheather = new Repository<WheatherPeriod>();
            var tmpRepVasttrafik = new Repository<VasttrafikIncident>();
            var mapInfoModel = new MapInfoVm
            {
                TrafficIncidents = _trafficRepository.FindAll().ToList(),
                WheatherPeriods = tmpRepWheather.FindAll().ToList(),
                VasttrafikIncidents = tmpRepVasttrafik.FindAll().ToList()
            };
            return mapInfoModel;
        }

        // GET api/carinrestful/5
        public Object GetInfoFromParams(string paramFoo, string paramBar)
        {
            return new { foo = paramFoo, bar = paramBar };
        }

        // POST api/carinrestful
        public void Post([FromBody]string value)
        {
        }

        // PUT api/carinrestful/5
        public void Put(int id, [FromBody]string value)
        {

        }

        // DELETE api/carinrestful/5
        public void Delete(int id)
        {

        }
    }
}