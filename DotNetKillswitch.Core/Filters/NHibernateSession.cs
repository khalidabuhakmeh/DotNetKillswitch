using System.Web;
using System.Web.Mvc;
using DotNetKillswitch.Core.Persistence;
using NHibernate;
using NHibernate.Context;

namespace DotNetKillswitch.Core.Filters
{
    public class NHibernateSession : ActionFilterAttribute
    {
        private readonly IKillswitchPersistence _database;

        public NHibernateSession()
        {
            _database = new KillswitchPersistence(new SqliteDatabase());   
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
           BindSession(HttpContext.Current);
        }
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
           UnbindSession(HttpContext.Current);
        }

        protected virtual void BindSession(HttpContext context)
        {
            ISession session = _database.OpenSession();

            // Tell NH session context to use it
            ManagedWebSessionContext.Bind(context, session);
        }

        protected virtual void UnbindSession(HttpContext context)
        {
            // Get the default NH session factory
            ISessionFactory factory = _database.GetSessionFactory();
            ISession session = ManagedWebSessionContext.Unbind(context, factory);

            try
            {
                if (session == null) return;

                session.Flush();
                session.Close();
            }
            catch
            {
                // No need to handle this for this piece.
            }
        }
    }
}
