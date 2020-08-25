using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LabbUppgift3.Models;
using LabbUppgift3.Infrastructure;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LabbUppgift3.Controllers
{

    public class CitizenController : Controller
    {
        private IErrandRep _repo;

        public CitizenController(IErrandRep repo)
        {
            _repo = repo;
        }

        // GET: /<controller>/
        public ViewResult Services()
        {
            return View();
        }

        public ViewResult Faq()
        {
            return View();
        }

        public ViewResult Contact()
        {
            return View();
        }

        // tar emot formuläret från startsidan, gör en koll om det är valid
        [HttpPost]
        public ViewResult Validate(Errand errand)
        {
            if (ModelState.IsValid)
            {                
                HttpContext.Session.SetJson("NewErrand", errand);
                return View(errand);
            }
            else
            {
                return View();
            }
        }

        public ViewResult Thanks()
        {
            // hämtar in sessionsdata
            var errand = HttpContext.Session.Getjson<Errand>("NewErrand");

            // sparar ärende i databasen
            _repo.SaveErrand(errand);

            // skickar RefNumber till vyn som viewbag
            ViewBag.Ref = errand.RefNumber;

            // ta död på sessionen
            HttpContext.Session.Remove("NewErrand");

            return View();
        }


    }
}
