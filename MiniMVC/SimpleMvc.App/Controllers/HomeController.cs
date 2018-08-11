using SimpleMvc.Framework.Attributes.Methods;
using SimpleMvc.Framework.Contracts;
using SimpleMvc.Framework.Controllers;

namespace SimpleMvc.App.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
