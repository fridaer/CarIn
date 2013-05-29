using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using CarIn.DAL.Repositories;
using CarIn.Models.Entities;

namespace CarIn.BLL
{
    public static class LoggHelper
    {
        private static Mutex mutext = new Mutex();
        private static readonly Repository<WebServiceLogg> _repository = new Repository<WebServiceLogg>();
        
        public static void SetLogg(string className,string statusCode, string message)
        {
            mutext.WaitOne();
            _repository.Add(new WebServiceLogg
                                {
                                    LogginTime = DateTime.Now,
                                    ClassName = className,
                                    StatusCode = statusCode,
                                    StatusMessag = message

                                });
            mutext.ReleaseMutex();
        }
        public static IEnumerable<WebServiceLogg> GetLoggs()
        {
            return _repository.FindAll();
        }
    }
}