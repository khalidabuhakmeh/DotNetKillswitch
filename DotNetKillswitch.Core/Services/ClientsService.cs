using System;
using System.Collections.Generic;
using System.Linq;
using DotNetKillswitch.Core.Persistence;

namespace DotNetKillswitch.Core.Services
{
    public class ClientsService : IClientsService
    {
        private readonly IRepository<ClientSite> _repository;

        public ClientsService( IRepository<ClientSite> repository)
        {
            _repository = repository;
        }

        public IList<ClientSite> Get()
        {
            return (from sites in _repository select sites).ToList();
        }

        public ClientSite Get(Guid id)
        {
            return (from site in _repository
                    where id == site.Id
                    select site).FirstOrDefault();
        }

        public void Add(ClientSite clientSite)
        {
            if (clientSite == null) 
                throw new ArgumentNullException("clientSite");

            if (clientSite.IsBlackListed)
                clientSite.LastTimeBlackListed = DateTime.Now;

             _repository.Add(clientSite);
        }

        public void Remove(Guid id)
        {
            var client = Get(id);
            _repository.Remove(client);
        }

        public void Update(ClientSite clientSite)
        {
            if (clientSite == null) 
                throw new ArgumentNullException("clientSite");

            if (clientSite.IsBlackListed)
                clientSite.LastTimeBlackListed = DateTime.Now;

            _repository.Add(clientSite);
        }

        public void ToggleBlackList(Guid id)
        {
            var client = Get(id);

            client.IsBlackListed = !client.IsBlackListed;
            
            if(client.IsBlackListed)
                client.LastTimeBlackListed = DateTime.Now;

            _repository.Add(client);            
        }

        public bool IsBlackListed(Guid id)
        {
            var client = Get(id);
            return client != null && client.IsBlackListed;
        }
    }
}