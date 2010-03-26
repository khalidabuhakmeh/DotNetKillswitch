using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DotNetKillswitch.Core.IoC;
using DotNetKillswitch.Web.IoC;
using Microsoft.Practices.Unity;

namespace DotNetKillswitch.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication, IKillswitchLocatorContainer 
    {
        private static IKillswitchServiceLocator _locator;

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{id}.kss");
            routes.IgnoreRoute("{fav}.ico");

            routes.MapRoute(
                "Default",                                              // Route name
                "{controller}/{action}/{id}",                           // URL with parameters
                new { controller = "ClientSites", action = "Index", id = "" }  // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            Locator = GetLocator();

            AreaRegistration.RegisterAllAreas();

            ControllerBuilder.Current.SetControllerFactory(new KillswitchControllerFactory());

            RegisterRoutes(RouteTable.Routes);
        }

        private static IKillswitchServiceLocator GetLocator()
        {
            var container = new UnityContainer().AddNewExtension<KilliswitchUnityExtension>();
            return new UnityKillswitchServiceLocator(container);
        }

        public IKillswitchServiceLocator Locator
        {
            get { return _locator; }
            private set { _locator = value; }
        }
    }
}