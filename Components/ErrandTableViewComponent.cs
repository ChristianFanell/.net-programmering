using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LabbUppgift3.Models;

namespace LabbUppgift3.Components
{
    
// public enum Roles {
//     Coordinator,
//     Admin,
//     Investigator
// }

    // view component för tabell
    public class ErrandTableViewComponent  : ViewComponent
    {
        private IErrandRep _repo;

        public ErrandTableViewComponent(IErrandRep repo)
        {
            _repo = repo;
        }

        // returnerar tabelldatan med ärenden, beroende på vilken controller som kallar på componenten
        public IViewComponentResult Invoke(string controller)
        {
            ViewBag.Controller = controller;
            ViewBag.Action = null;
            IQueryable<Errand> data = null;
            string returnView = "ErrandTable";

            switch (controller)
            {
                case "Manager":
                    data = _repo.GetManagerErrands;
                    ViewBag.Action = "CrimeManager";
                    break;
                case "Coordinator":
                    data = _repo.Errands;
                    ViewBag.Action = "CrimeCoordinator";
                    break;
                case "Investigator":
                    data =  _repo.GetErrandByInvestigator;                    
                    ViewBag.Action = "CrimeInvestigator";
                    break;
                default:
                    break;
            }
            return View(returnView, data);//skapa vy
        }
    }
}
