using System;
using System.Threading;
using Itac.OemAccess.TestingServer.BuisnessLogic;
using Microsoft.Owin.Hosting;
using Microsoft.Practices.Unity;

namespace Itac.OemAccess.TestingServer
{
    class Program
    {
        private static readonly AutoResetEvent CtrlCEvent = new AutoResetEvent(false);
        static void Main()
        {
            var ioc = new UnityContainer();
            Microsoft.Practices.ServiceLocation.ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(ioc));
            ioc.RegisterType<Devices>(new ContainerControlledLifetimeManager());

            ioc.RegisterType<ApplicationRepository>(new ContainerControlledLifetimeManager());
            ioc.RegisterType<PlatformRepository>(new ContainerControlledLifetimeManager());
            ioc.RegisterType<GlobalRepository>(new ContainerControlledLifetimeManager());
            ioc.RegisterType<AreaManager>(new ContainerControlledLifetimeManager());

            WebApiBuilder.UnityContainer = ioc;
            var options = new StartOptions();
            options.Urls.Add("http://+:8080");
            WebApp.Start<WebApiBuilder>(options);
            Log.Trace("API Service started");
            foreach (var optionsUrl in options.Urls)
            {
                Log.Trace("   " + optionsUrl);
            }

            options = new StartOptions();
            options.Urls.Add("http://+:8081");
            WebApp.Start<SignalRStartup>(options);
            Log.Trace("Control hub service started");
            foreach (var optionsUrl in options.Urls)
            {
                Log.Trace("   " + optionsUrl);
            }

            Log.Trace("System running <ctrl-C> to exit ...");
            Console.CancelKeyPress += (sender, eventArgs) => CtrlCEvent.Set();
            CtrlCEvent.WaitOne();
        }

    }
}
