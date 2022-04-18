using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using SynapseWebAPI.Models;

namespace SynapseWebAPI.Controllers
{
    [ApiVersion("1.0")]
    public class CustomersController : ODataController
    {
        private readonly IQuerySynapse querySynapse;

        public CustomersController(IQuerySynapse querySynapse)
        {
            this.querySynapse = querySynapse;
        }

        [EnableQuery(PageSize = 1000)]
        public ActionResult Get(ODataQueryOptions<DimCustomer> query)
        {
            return Ok(this.querySynapse.Query(query));
        }
    }
}
