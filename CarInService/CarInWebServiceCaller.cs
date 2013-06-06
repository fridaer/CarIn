using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CarIn.BLL;
using CarIn.DAL.Context;
using SUI.Helpers;

namespace CarInService
{
    public partial class CarInWebServiceCaller : ServiceBase
    {
        private HandlerForWebServiceCalls _handlerForWebServiceCalls; 

        public CarInWebServiceCaller()
        {
            InitializeComponent();
            if (!System.Diagnostics.EventLog.SourceExists("MySource"))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                    "MySource", "MyNewLog");
            }

            CarInEventLogger.Source = "MySource";
            CarInEventLogger.Log = "MyNewLog";
            CarInEventLogger.Clear();
            _handlerForWebServiceCalls = new HandlerForWebServiceCalls(CarInEventLogger);

        }

        protected override void OnStart(string[] args)
        {
            CarInEventLogger.WriteEntry("Begining Timers");
            try
            {
                _handlerForWebServiceCalls.BeginTimers();

            }
            catch (Exception e)
            {
                CarInEventLogger.WriteEntry(e.Message);
                CarInEventLogger.WriteEntry(e.InnerException.Message);
                CarInEventLogger.WriteEntry(e.InnerException.HelpLink);



            }
        }

        protected override void OnStop()
        {
            CarInEventLogger.WriteEntry("Stopping Timers");
            _handlerForWebServiceCalls.StopTimers();
        }
    }
}
