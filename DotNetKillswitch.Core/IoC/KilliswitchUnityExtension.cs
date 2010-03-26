using DotNetKillswitch.Core.Persistence;
using DotNetKillswitch.Core.Services;
using Microsoft.Practices.Unity;

namespace DotNetKillswitch.Core.IoC
{
    public class KilliswitchUnityExtension: UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.RegisterType<IRepository<ClientSite>, GenericRepository<ClientSite>>()
                .RegisterType<IDatabaseResolver, SqliteDatabase>(new ContainerControlledLifetimeManager())
                .RegisterType<IClientsService, ClientsService>()
                .RegisterType<IKillswitchPersistence, KillswitchPersistence>();
        }
    }
}