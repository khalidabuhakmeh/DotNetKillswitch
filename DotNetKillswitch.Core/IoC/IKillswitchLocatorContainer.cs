using System;
using System.Web.Mvc;

namespace DotNetKillswitch.Core.IoC
{
    public interface IKillswitchLocatorContainer
    {
        IKillswitchServiceLocator Locator { get; }
    }
}
