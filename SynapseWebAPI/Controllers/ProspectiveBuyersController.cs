using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace SynapseWebAPI.Controllers
{
    [ApiVersion("1.0")]
    public class ProspectiveBuyersController : ODataController
    {
        private readonly IQuerySynapse querySynapse;

        public ProspectiveBuyersController(IQuerySynapse querySynapse)
        {
            this.querySynapse = querySynapse;
        }

        [EnableQuery(PageSize = 1000)]
        public ActionResult Get(ODataQueryOptions<ProspectiveBuyer> query)
        {
            return Ok(this.querySynapse.Query(query));
        }
    }
}
