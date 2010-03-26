using System;
using System.Web.Mvc;
using System.Web.Routing;
using DotNetKillswitch.Core.IoC;

namespace DotNetKillswitch.Web.IoC
{
   public class KillswitchControllerFactory : DefaultControllerFactory
    {
        public override IController CreateController(RequestContext context, string controllerName)
        {
            Type type = GetControllerType(context, controllerName);
 
            if (type == null)
            {
                throw new InvalidOperationException(string.Format("Could not find a controller with the name {0}", controllerName));
            }

            IKillswitchServiceLocator container = GetContainer(context);
            return (IController)container.Resolve(type);
        }

        protected virtual IKillswitchServiceLocator GetContainer(RequestContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
 
            var killswitchLocatorContainer = context.HttpContext.ApplicationInstance as IKillswitchLocatorContainer;
            if (killswitchLocatorContainer == null)
            {
                throw new InvalidOperationException(
                    "You must extend the HttpApplication in your web project and implement the IContainerAccessor to properly expose your container instance");
            }
 
            IKillswitchServiceLocator container = killswitchLocatorContainer.Locator;
            if (container == null)
            {
                throw new InvalidOperationException("The container seems to be unavailable in your HttpApplication subclass");
            }

            return container;
        }
    }
}