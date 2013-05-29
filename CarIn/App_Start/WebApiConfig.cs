using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Dependencies;
using CarIn.Controllers;
using CarIn.DAL.Repositories;
using CarIn.DAL.Repositories.Abstract;
using CarIn.Models.Entities;

namespace CarIn
{
    public static class WebApiConfig
    {
        class SimpleContainer : IDependencyResolver
        {

            static readonly IRepository<TrafficIncident> trafficRespository = new Repository<TrafficIncident>();
            static readonly IRepository<WheatherPeriod> wheatherRespository = new Repository<WheatherPeriod>();
            static readonly IRepository<VasttrafikIncident> vasttrafikRespository = new Repository<VasttrafikIncident>();
            static readonly IRepository<MapQuestDirection> directionsRepository = new Repository<MapQuestDirection>();


            public IDependencyScope BeginScope()
            {
                // This example does not support child scopes, so we simply return 'this'.
                return this;
            }

            public object GetService(Type serviceType)
            {
                if (serviceType == typeof(CarInRESTfulController))
                {
                    return new CarInRESTfulController(trafficRespository, wheatherRespository, vasttrafikRespository,directionsRepository );
                }
                    return null;
            }

            public IEnumerable<object> GetServices(Type serviceType)
            {
                return new List<object>();
            }

            public void Dispose()
            {
                // When BeginScope returns 'this', the Dispose method must be a no-op.
            }
        }
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/v1/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.DependencyResolver = new SimpleContainer();
            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);

            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            //config.EnableQuerySupport();
        }
    }
}