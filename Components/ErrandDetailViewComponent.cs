using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LabbUppgift3.Models;

namespace LabbUppgift3.Components
{
    // viewcomponent
    public class ErrandDetailViewComponent : ViewComponent
    {
        private IErrandRep _repo;

        // constructor, hämtar in modellen som en ramverkstjänst
        public ErrandDetailViewComponent(IErrandRep repo)
        {
            _repo = repo;
        }

        // returnerar tillhörande vy för denna komponent med data som hämtas med id
        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            var ErrandObject = await _repo.GetErrand(id);
            
            return View("ErrandDetail", ErrandObject);//skapa vy
        }
    }
}
