using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;
using CarIn.DAL.Repositories;
using CarIn.DAL.Repositories.Abstract;
using CarIn.Models.Entities;
using Ninject;

namespace CarIn.App_Start
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private readonly IKernel _kernel;

        public NinjectControllerFactory()
        {
            _kernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if(controllerType == null)
            {
                return null;
            }
            return _kernel.Get(controllerType) as IController;
        }

        private void AddBindings()
        {
            _kernel.Bind(typeof(IRepository<>)).To(typeof(Repository<>));
        }
    }
}