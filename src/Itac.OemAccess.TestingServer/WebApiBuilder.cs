using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;
using CacheCow.Server;
using Itac.OemAccess.TestingServer.Authentication;
using Owin;
using Unity;
using Unity.AspNet.WebApi;

namespace Itac.OemAccess.TestingServer
{
    public class WebApiBuilder
    {
        public static IUnityContainer UnityContainer { get; set; }

        public static void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            var config = new HttpConfiguration()
            {
                DependencyResolver = new UnityDependencyResolver(UnityContainer)
            };

            //Adding CacheCow
            var objCacheCow = new CacheCow.Server.CachingHandler(config, "");
            UnityContainer.RegisterInstance<ICachingHandler>(objCacheCow);

            config.MessageHandlers.Add(objCacheCow);
            config.MapHttpAttributeRoutes(new CustomDirectRouteProvider());
            config.EnableCors();
            config.EnsureInitialized();

            appBuilder.Use<HmacAuthenticationMiddleware>(new HmacAuthenticationOptions(UnityContainer));
            appBuilder.UseWebApi(config);

            //Route Debugging
            //var routes = config.Routes
            //    .Select(route => (IEnumerable)route)
            //    .Single(route => route != null)
            //    .Cast<HttpRoute>();

            //var manifest = routes
            //    .Select(route => route.RouteTemplate)
            //    .ToList();

        }
    }
    public class CustomDirectRouteProvider : DefaultDirectRouteProvider
    {
        protected override IReadOnlyList<IDirectRouteFactory> GetActionRouteFactories(HttpActionDescriptor actionDescriptor)
        {
            return actionDescriptor.GetCustomAttributes<IDirectRouteFactory>(inherit: true);
        }
    }
}
