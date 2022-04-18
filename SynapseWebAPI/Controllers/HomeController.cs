using Microsoft.AspNetCore.Mvc;

namespace SynapseWebAPI.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {
        private readonly IQuerySynapse querySynapse;

        public HomeController(IQuerySynapse querySynapse)
        {
            this.querySynapse = querySynapse;
        }

        // GET: HomeController
        public ActionResult Index()
        {
            this.querySynapse.Query();
            return View();
        }
    }
}
