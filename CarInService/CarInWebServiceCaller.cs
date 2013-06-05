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

namespace CarInService
{
    public partial class CarInWebServiceCaller : ServiceBase
    {

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
        }

        protected override void OnStart(string[] args)
        {
            CarInEventLogger.WriteEntry("Startar");
            Thread.Sleep(10000);
            try
            {

                using(var context = new CarInContext())
                {
                    CarInEventLogger.WriteEntry(context.WheatherPeriods.Select(x => x.SymbolName).First());

                }

            }
            catch (Exception e)
            {
                CarInEventLogger.WriteEntry(e.Message);
                CarInEventLogger.WriteEntry(e.InnerException.Message);
                CarInEventLogger.WriteEntry(e.InnerException.HelpLink);



            }
                //
            //handlerForWebServiceCalls = new HandlerForWebServiceCalls();
            //handlerForWebServiceCalls.BeginTimers();
        }

        protected override void OnStop()
        {
            //LoggHelper.SetLogg("CarinWebService", "Stop", "stopar");

            //handlerForWebServiceCalls.StopTimers();
        }
    }
}
