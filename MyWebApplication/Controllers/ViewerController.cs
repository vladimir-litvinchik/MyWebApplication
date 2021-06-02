using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MyWebApplication.Controllers
{
    public class ViewerController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
