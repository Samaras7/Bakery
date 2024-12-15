using Microsoft.AspNetCore.Mvc;

namespace Bakery.Controllers
{
    public class ContactController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}
