using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarIn.Models.Entities.Abstract;

namespace CarIn.Models.Entities
{
    public class WebServiceLogg : IEntity
    {
        public int ID { get; set; }
        public DateTime LogginTime { get; set; }
        public string ClassName { get; set; }
        public string StatusCode { get; set; }
        public string StatusMessag { get; set; }
    }
}