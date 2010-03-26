#region License

//
// Author: Javier Lozano <javier@lozanotek.com>
// Copyright (c) 2009-2010, lozanotek, inc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//   http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//

#endregion

using DotNetKillswitch.Core.IoC;
using DotNetKillswitch.Core.Persistence;
using System;
using System.Web;
using NHibernate;
using NHibernate.Context;

namespace DotNetKillswitch.Core.Web {
    /// <summary>
    /// Defines the getting and setting of <see cref="ISession"/> for the request lifetime.
    /// </summary>
    public class NHibernateSessionModule : IHttpModule {
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="database"></param>
        public NHibernateSessionModule() {
            var container =  HttpContext.Current.ApplicationInstance as IKillswitchLocatorContainer;
            Database = container.Locator.Resolve<IKillswitchPersistence>();
        }

        /// <summary>
        /// Gets the specified <see cref="IKillswitchPersistence"/> to use.
        /// </summary>
        public IKillswitchPersistence Database { get; private set; }

        #region IHttpModule Members

        public void Init(HttpApplication context) {
            context.BeginRequest += BeginRequest;
            context.EndRequest += EndRequest;
        }

        public void Dispose() {
        }

        #endregion

        public virtual void BeginRequest(object sender, EventArgs e) {
            var application = (HttpApplication) sender;
            HttpContext context = application.Context;

            BindSession(context);
        }

        protected virtual void BindSession(HttpContext context) {
            ISession session = Database.OpenSession();

            // Tell NH session context to use it
            ManagedWebSessionContext.Bind(context, session);
        }

        public virtual void EndRequest(object sender, EventArgs e) {
            var application = (HttpApplication) sender;
            HttpContext context = application.Context;

            UnbindSession(context);
        }

        protected virtual void UnbindSession(HttpContext context) {
            // Get the default NH session factory
            ISessionFactory factory = Database.GetSessionFactory();
            ISession session = ManagedWebSessionContext.Unbind(context, factory);

            try {
                // Give it to NH so it can pull the right session

                if (session == null) return;

                session.Flush();
                session.Close();
            }
            catch {
                // No need to handle this for this piece.
            }
        }
    }
}