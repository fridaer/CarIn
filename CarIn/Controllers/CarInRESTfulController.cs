using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;
using CarIn.DAL.Repositories.Abstract;
using CarIn.Models.Entities;
using Newtonsoft.Json.Linq;

namespace CarIn.Controllers
{
    public class CarInRESTfulController : ApiController
    {
        public Object GetAllInfo()
        {
            var tmpObj = new {foo = "Foo", bar = "Bar"};

            return tmpObj;
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
