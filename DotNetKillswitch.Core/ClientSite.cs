using System;

namespace DotNetKillswitch.Core
{
    public class ClientSite
    {
        public virtual Guid Id { get; set; }        
        public virtual string Name { get; set; }
        public virtual bool IsBlackListed { get; set; }
        public virtual DateTime? LastTimeBlackListed { get; set; }
    }
}
