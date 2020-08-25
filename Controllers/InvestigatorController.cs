using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LabbUppgift3.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LabbUppgift3.Controllers
{
    [Authorize(Roles = "Investigator")]
    public class InvestigatorController : Controller
    {

        private IErrandRep _repo;
        private IHostingEnvironment environment;
        private IHttpContextAccessor contextAcc;

        // constructor, tar in interface från .net-ramverkets tjänster
        public InvestigatorController(IErrandRep repo, IHostingEnvironment env, IHttpContextAccessor cont)
        {
            _repo = repo;
            environment = env;
            contextAcc = cont;
        }


        // async hjälpfunktion för att skippa dubbelkod
        // noterar att utan async skiter sig uppladdningen då bara en liten del av bilden tankas upp
        private async Task MoveFileToFolder(IFormFile file, string folderName)
        {                      
            var tempPath = Path.GetTempFileName();

            using (var stream = new FileStream(tempPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            // skapa folder och flytta över till folderName
            var imgPath = Path.Combine(environment.WebRootPath, folderName, file.FileName);

            System.IO.File.Move(tempPath, imgPath);
        }

        // controller metod för att ladda upp filer och uppdera ärende
        // kan skippa async här om jag ändrar ovan hjälpmetods returvärde till void
        [HttpPost]
        public async Task<IActionResult> SaveInvestigator(IFormFile loadImage, IFormFile loadSample, Errand errand, Status status)
        {
            var id = TempData["id"].ToString();

            if (loadImage != null && loadImage.Length > 0)
            {
                 await MoveFileToFolder(loadImage, "uploadsImg");
                _repo.SaveFileToDb(loadImage.FileName, true, id);
            }

            if (loadSample != null && loadSample.Length > 0)
            {
                await MoveFileToFolder(loadSample, "uploads");
                _repo.SaveFileToDb(loadSample.FileName, false, id);
            }

            _repo.SaveComments(errand, id);

            return RedirectToAction("StartInvestigator", _repo);
        }

        // returnerar vy med data i viewbags. data till tabellen hämtas i en view component
        public ViewResult StartInvestigator()
        {
            // hämtar användarnamn
            var loggedIn = contextAcc.HttpContext.User.Identity.Name;
            // hämtar förnamn på användare
            ViewBag.UserName = _repo.Employees.Where(e => e.EmployeeId == loggedIn).First().Name.Split(" ")[0];
            ViewBag.Controller = "Investigator";
            // hämtar samtliga statusar
            ViewBag.Statuses = _repo.Statuses;
            return View();
        }

        // detaljvy, tar emot id som skickas till vyn som viewbag samt skickar dataobjektet
        public ViewResult CrimeInvestigator(string id)
        {
            ViewBag.ID = id;
            ViewBag.Status = _repo.Statuses;
            ViewBag.Repo = _repo;

            //tempdata för att kunna spara ärendes department
            TempData["id"] = id;

            return View();
        }
    }
}
