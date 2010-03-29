using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DotNetKillswitch.Core.Client;

namespace DotNetKillswitch.BadClient
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default",                                              // Route name
                "{controller}/{action}/{id}",                           // URL with parameters
                new { controller = "Home", action = "Index", id = "" }  // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            
            RegisterRoutes(RouteTable.Routes);

            Killswitch.Set()
                .WithServer(new Uri("http://localhost:8357/"))
                .WithClientId(new Guid("e3161bcb-4c52-4dad-985d-e4b01ba06444"));
        }
    }
}