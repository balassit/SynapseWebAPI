using Microsoft.AspNetCore.OData.Query;
using SynapseWebAPI.Models;

namespace SynapseWebAPI
{
    public interface IQuerySynapse
    {
        IQueryable<DimCustomer> Query(ODataQueryOptions<DimCustomer> query);

        IQueryable<ProspectiveBuyer> Query(ODataQueryOptions<ProspectiveBuyer> query);
    }
}