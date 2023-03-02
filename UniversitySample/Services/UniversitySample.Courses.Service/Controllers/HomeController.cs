using GraphQL.Server.Transports.AspNetCore;
using GraphQL.Server.Ui.GraphiQL;
using Microsoft.AspNetCore.Mvc;

namespace UniversitySample.Courses.Service.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
            => new GraphiQLActionResult(opts =>
            {
                opts.GraphQLEndPoint = "/Home/graphql";
                opts.SubscriptionsEndPoint = "/Home/graphql";
            });

        [HttpGet]
        [HttpPost]
        [ActionName("graphql")]
        public IActionResult GraphQL()
            => new GraphQLExecutionActionResult();
    }
}
