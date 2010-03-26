using System;
using System.Web.Mvc;

namespace DotNetKillswitch.Core.IoC
{
    public interface IKillswitchServiceLocator
    {
        T Resolve<T>();
        object Resolve(Type type);
    }
}
