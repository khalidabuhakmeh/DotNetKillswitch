using System;
using Microsoft.Practices.Unity;

namespace DotNetKillswitch.Core.IoC
{
    public class UnityKillswitchServiceLocator : IKillswitchServiceLocator
    {
        private readonly IUnityContainer _container;

        public UnityKillswitchServiceLocator(IUnityContainer container)
        {
            _container = container;
        }

        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        public object Resolve(Type type)
        {
            return _container.Resolve(type);
        }
    }
}