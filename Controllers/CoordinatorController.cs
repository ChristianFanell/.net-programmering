using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LabbUppgift3.Models;
using LabbUppgift3.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace LabbUppgift3.Controllers
{
    [Authorize(Roles = "Coordinator")]
    public class CoordinatorController : Controller
    {

        private IErrandRep _repo;

        // constructor, tar in ett interface från .net-ramverkets tjänster som en tjänst (DI)
        public CoordinatorController(IErrandRep repo)
        {
            _repo = repo;
        }



        // GET: /<controller>/
        
        // returnerar en vy med data från min model i viewbags. Data till tabellen laddas in från en view component
        public ViewResult StartCoordinator()
        {
            ViewBag.Controller = "Coordinator";
            ViewBag.Statuses = _repo.Statuses;
            ViewBag.DepartmentList = _repo.DepartmentList;
            return View();
        }

        public ViewResult ReportCrime()
        {
            var errand = HttpContext.Session.Getjson<Errand>("NewErrand");

            // returnerar vy utan data om ej session
            if (errand == null)
            {
                return View();
            }

            // returnerar vy med sessionsdata från tidigare formulärifyllning
            else
            {
                return View(errand);
            }
        }

   

        public ViewResult Thanks()
        {
            //hämtar in session
            var errand = HttpContext.Session.Getjson<Errand>("NewErrand");

            //sparar sessionsobjektet i databasen som nytt Errand
            _repo.SaveErrand(errand);

            // skickar objektets refnumber till vyn
            ViewBag.Ref = errand.RefNumber;

            // ta död på sessionen
            HttpContext.Session.Remove("NewErrand");

            return View();
        }

        // sparar uppdatering av department  i ett ärende
        // meningslös validering då jag validerar med javascript, roligare så :)
        [HttpPost]
        public IActionResult SaveDepartment(Department dep)
        {
            var id = TempData["id"].ToString();

            if (dep.DepartmentId == "D00" || dep.DepartmentId == "Välj")
            {
                return RedirectToAction("StartCoordinator");
            }
            // spara
            else
            {
                _repo.UpdateErrandDepartment(id, dep.DepartmentId);
                return RedirectToAction("StartCoordinator");
            }

        }

        // detaljsida för ärende, tar emot ett id och returnerar det som viewbag (UTAN) med själva repot också
        // id skickas sedan in i en component i vyn
        public ViewResult CrimeCoordinator(string id)
        {
            // viewbags för dataåtkomst i vyn
            ViewBag.ID = id;
            ViewBag.ListOfDepartments = _repo.DepartmentList;

            //tempdata för att kunna spara ärendes department
            TempData["id"] = id;

            //return View(_repo);
            // skippar att skicka med repo, i vyn anger jag modellen Department istället
            return View();
        }


        // tar emot formuläret från CoordinatorReportCrime, gör en koll om det är valid
        [HttpPost]
        public ViewResult Validate(Errand errand)
        {
            //ViewBag.Title = 
            if (ModelState.IsValid)
            {
                // skapar en session av inkommande objekt, om objekt
                HttpContext.Session.SetJson("NewErrand", errand);

                return View(errand);
            }
            // annars returneras bara vyn
            else
            {
                return View();
            }

        }
    }
}
