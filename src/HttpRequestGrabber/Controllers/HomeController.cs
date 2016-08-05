using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HttpRequestGrabber.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var requests = RequestStore.GetAll().ToList();
            return View(requests);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
