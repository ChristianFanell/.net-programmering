using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LabbUppgift3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LabbUppgift3.Controllers
{
    [Authorize(Roles = "Manager")]
    public class ManagerController : Controller
    {

        private IErrandRep _repo;
        private IHttpContextAccessor contextAcc;

        // constructor, hämtar in interface från startup.cs. DI
        public ManagerController(IErrandRep repo, IHttpContextAccessor cont)
        {
            _repo = repo;
            contextAcc = cont;
        }

        // returnerar detaljvy som i sin tur hämtar in viewcomponent
        // GET: /<controller>/
        public ViewResult CrimeManager(string id)
        {
            ViewBag.ID = id;
            TempData["id"] = id;
            ViewBag.ListOfEmployees = _repo.GetManagersInvestigators;
            return View();
        }
       
        // för redigering av ärende
        [HttpPost]
        public IActionResult EditErrand(Employee emp)
        {
            var id = TempData["id"].ToString();

            if (emp.NoAction)
            {
                _repo.UpdateErrandEmployee(id, emp.NotInvestigate, emp.NoAction);
            }
            else if (!emp.NoAction)
            {
                _repo.UpdateErrandEmployee(id, emp.EmployeeId, emp.NoAction);
            }

            return RedirectToAction("StartManager");
        }

        //returnerar vy med data
        public ViewResult StartManager()
        {
            // hämtar användarnamn
            var loggedIn = contextAcc.HttpContext.User.Identity.Name;
            // hämtar förnamnet
            ViewBag.UserName = _repo.Employees.Where(e => e.EmployeeId == loggedIn).First().Name.Split(" ")[0];
            ViewBag.Controller = "Manager";
            // hämtar in alla statusar
            ViewBag.Statuses = _repo.Statuses;
            // hämtar alla chefens underställda
            ViewBag.Employees = _repo.GetManagersInvestigators;
            return View();
        }

      

    }
}
