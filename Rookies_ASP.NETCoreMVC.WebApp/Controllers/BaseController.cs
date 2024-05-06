using Microsoft.AspNetCore.Mvc;

namespace Rookies_ASP.NETCoreMVC.WebApp.Controllers
{
    public class BaseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
