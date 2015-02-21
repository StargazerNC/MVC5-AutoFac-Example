using System;
using System.Linq;
using System.Web.Mvc;
using Contracts;
using DTOs;

namespace Site.Controllers
{
    public class HomeController : Controller
    {
        private readonly IService mService = null;
        public HomeController(IService service)
        {
            mService = service;
        }
        public ActionResult Index()
        {
            UserDTO dto = new UserDTO();
            @ViewBag.Version = mService.GetVersion();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}