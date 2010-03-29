using System;
using System.Web;
using DotNetKillswitch.Core.IoC;
using DotNetKillswitch.Core.Persistence;
using DotNetKillswitch.Core.Services;
using NHibernate;
using NHibernate.Context;

namespace DotNetKillswitch.Core.Web
{
    public class DotNetKillswitchHandler : IHttpHandler 
    {
        private readonly IClientsService _clientService;
        private readonly IKillswitchPersistence _persistence;

        public DotNetKillswitchHandler()
        {
            var container = (IKillswitchLocatorContainer) HttpContext.Current.ApplicationInstance;

            if (container == null)
                throw new ArgumentException("the current application does not implement IKillswitchLocatorContainer.");

            _clientService = container.Locator.Resolve<IClientsService>();
            _persistence = container.Locator.Resolve<IKillswitchPersistence>();
        }

        public void ProcessRequest(HttpContext context)
        {            
            try
            {
                BindSession(context);

                var query = context.Request.Path.Remove(0, 1);
            
                query = query.Replace(Constants.Prefix, string.Empty);

                if (!string.IsNullOrEmpty(query))
                {
                    var id = Guid.Empty;

                    if (!Guid.TryParse(query, out id) || !_clientService.IsBlackListed(id))
                        return;

                    // this is a css blackout
                    context.Response.ContentType = Constants.CssContentType;
                    context.Response.Write(Constants.Css);
                }
            }
            catch (Exception)
            {
                context.Response.Write(string.Empty);
            }
            finally
            {
                UnbindSession(context);
            }

            context.Response.Flush();
            context.Response.End();
        }

        protected virtual void BindSession(HttpContext context)
        {
            ISession session = _persistence.OpenSession();

            // Tell NH session context to use it
            ManagedWebSessionContext.Bind(context, session);
        }

        protected virtual void UnbindSession(HttpContext context)
        {
            // Get the default NH session factory
            ISessionFactory factory = _persistence.GetSessionFactory();
            ISession session = ManagedWebSessionContext.Unbind(context, factory);
            
            try
            {
                // Give it to NH so it can pull the right session
                if (session == null) return;

                session.Flush();
                session.Close();
            }
            catch
            {
                // No need to handle this for this piece.
            }
        }

        public bool IsReusable
        {
            get { return true; }
        }
    }
}
