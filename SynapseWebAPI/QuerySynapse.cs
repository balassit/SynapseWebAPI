using Microsoft.AspNetCore.OData.Query;
using SynapseWebAPI.Models;

namespace SynapseWebAPI
{
    public class QuerySynapse : IQuerySynapse
    {
        private readonly IServiceProvider serviceProvider;
        public QuerySynapse(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IQueryable<ProspectiveBuyer> Query(ODataQueryOptions<ProspectiveBuyer> query)
        {
            AzureSynapseContext context = serviceProvider.GetService<AzureSynapseContext>();
            IQueryable? items = query.ApplyTo(from buyer in context.ProspectiveBuyer select buyer);

            return (IQueryable<ProspectiveBuyer>)items;
        }

        public IQueryable<DimCustomer> Query(ODataQueryOptions<DimCustomer> query)
        {
            AzureSynapseContext context = serviceProvider.GetService<AzureSynapseContext>();
            IQueryable? items = query.ApplyTo(from customer in context.Customers select customer);

            return (IQueryable<DimCustomer>)items;
        }
    }
}
