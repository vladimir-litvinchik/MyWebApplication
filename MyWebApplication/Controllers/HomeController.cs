using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace MyWebApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var files = Directory
                .GetFiles(Server.MapPath("~/Files/"))
                .Select(file => Path.GetFileName(file));

            return View(files);
        }
    }
}
