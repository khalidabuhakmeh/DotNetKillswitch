using System.IO;
using DotNetKillswitch.Core.Persistence.Mappings;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.ByteCode.Castle;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace DotNetKillswitch.Core.Persistence
{
    /// <summary>
    /// Default implementation of <see cref="IKillswitchPersistence"/>.
    /// </summary>
    public class KillswitchPersistence : IKillswitchPersistence
    {
        private static readonly object _lock = new object();
        private static Configuration _configuration;
        private static ISessionFactory _sessionFactory;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="databaseResolver"></param>
        public KillswitchPersistence(IDatabaseResolver databaseResolver)
        {
            CurrentDatabaseResolver = databaseResolver;
        }

        /// <summary>
        /// See <see cref="IKillswitchPersistence.CurrentDatabaseResolver"/>.
        /// </summary>
        public IDatabaseResolver CurrentDatabaseResolver { get; private set; }

        /// <summary>
        /// Create the <see cref="Configuration"/> to use for the database.
        /// </summary>
        /// <returns></returns>
        private Configuration GetConfiguration()
        {
            lock (_lock)
            {
                if (_configuration == null)
                {
                    lock (_lock)
                    {
                        var filePath = CurrentDatabaseResolver.FilePath;
                        SQLiteConfiguration liteConfiguration =
                            SQLiteConfiguration.Standard
                                .UsingFile(filePath)
                                .ProxyFactoryFactory(typeof(ProxyFactoryFactory));

                        _configuration =
                            Fluently
                                .Configure()
                                .Database(liteConfiguration)
                                .Mappings(m => m.FluentMappings.Add<ClientSiteMapping>())                                
                            // Install the database if it doesn't exist
                                .ExposeConfiguration(config =>
                                {
                                    if (File.Exists(filePath)) return;

                                    SchemaExport export = new SchemaExport(config);
                                    export.Drop(false, true);
                                    export.Create(false, true);
                                })
                                .BuildConfiguration();

                        if (!_configuration.Properties.ContainsKey("current_session_context_class"))
                        {
                            _configuration.Properties.Add("current_session_context_class", "managed_web");
                        }
                    }
                }

                return _configuration;
            }
        }

        /// <summary>
        /// See <see cref="IKillswitchPersistence.GetSessionFactory"/>.
        /// </summary>
        /// <returns></returns>
        public ISessionFactory GetSessionFactory()
        {
            if (_sessionFactory == null)
            {
                var config = GetConfiguration();
                _sessionFactory = config.BuildSessionFactory();
            }

            return _sessionFactory;
        }

        /// <summary>
        /// See <see cref="IKillswitchPersistence.OpenSession"/>.
        /// </summary>
        /// <returns></returns>
        public ISession OpenSession()
        {
            ISessionFactory factory = GetSessionFactory();
            return factory.OpenSession();
        }

        /// <summary>
        /// See <see cref="IKillswitchPersistence.GetCurrentSession"/>.
        /// </summary>
        /// <returns></returns>
        public ISession GetCurrentSession()
        {
            ISessionFactory factory = GetSessionFactory();
            return factory.GetCurrentSession();
        }
    }
}
