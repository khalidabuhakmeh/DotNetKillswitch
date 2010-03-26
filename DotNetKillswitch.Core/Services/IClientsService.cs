using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetKillswitch.Core.Persistence;

namespace DotNetKillswitch.Core.Services
{
    public interface IClientsService
    {
        IList<ClientSite> Get();
        ClientSite Get(Guid id);        
        void Add(ClientSite clientSite);
        void Remove(Guid id);
        void Update(ClientSite clientSite);
        void ToggleBlackList(Guid id);
        bool IsBlackListed(Guid id);
    }
}
