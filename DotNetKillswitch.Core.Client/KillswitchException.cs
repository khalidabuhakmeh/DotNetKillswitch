using System;

namespace DotNetKillswitch.Core.Client
{
    public class KillswitchException : Exception
    {
        public KillswitchException(string message)
            :base(message)
        {}
    }
}