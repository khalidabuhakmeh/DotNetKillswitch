using FluentNHibernate.Mapping;

namespace DotNetKillswitch.Core.Persistence.Mappings
{
    class ClientSiteMapping: ClassMap<ClientSite> {
        public ClientSiteMapping() {
            // Specify the table to use
            Table("ClientSites");

            // Sets the key for the table to use
            Id(x => x.Id)
                .GeneratedBy
                .Guid();
            
            // Maps all the columns
            Map(x => x.Name);
            Map(x => x.IsBlackListed);
            Map(x => x.LastTimeBlackListed);
        }
    }
}
