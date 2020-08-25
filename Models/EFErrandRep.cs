using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace LabbUppgift3.Models
{
    public class EFErrandRep : IErrandRep
    {
        private AppDbContext _context;
        private IHttpContextAccessor contextAcc;

        // konstruktor där db-data och inloggningsinformation skjuts in via DI
        public EFErrandRep(AppDbContext context, IHttpContextAccessor cont)
        {
            _context = context;
            contextAcc = cont;
        }

        // kopplar modell till databas

        // uppdaterar denna så att den inkluderar relaterade tabeller. Detta är samma sak som att i en sql query använda join
        public IQueryable<Errand> Errands => _context.Errands
                                            .Include(td => td.Samples)
                                            .Include(td => td.Pictures)
                                            .Include(td => td.Department)
                                            .Include(td => td.Employee)
                                            .Include(td => td.Status);


        // KOD SOM RETURNERAR DATA FÖR MANAGER INLOGGNINGEN

        // returnerar anställda som är från managers avdelning
        public IQueryable<Employee> GetManagersInvestigators {
            get {
                // hämtar inloggad användare
                var userName = contextAcc.HttpContext.User.Identity.Name;
                // hämtar korrekt avdelning som manager tillhör
                var department = _context.Employees.Where(td => td.EmployeeId == userName).First();

                // returnerar alla anställda på avdelningen, utom chefen själv såklart
                return _context.Employees
                        .Where(td => td.DepartmentId == department.DepartmentId && td.EmployeeId != userName);
            } 
        }

        // returnerar ärenden med managerns avdelning
        public IQueryable<Errand> GetManagerErrands
        {
            get
            {
                // hämtar inloggad användare
                var userName = contextAcc.HttpContext.User.Identity.Name;
                // hämtar korrekt avdelning som manager tillhör
                var department = _context.Employees.Where(td => td.EmployeeId == userName).First();
                var data = _context.Errands
                             .Where(td => td.DepartmentId == department.DepartmentId)
                            .Include(td => td.Samples)
                            .Include(td => td.Pictures)
                            .Include(td => td.Department)
                            .Include(td => td.Employee)
                            .Include(td => td.Status);
                return data;
            }
        }

        // KOD SOM RETURNERAR DATA FÖR INVESTIGATOR INLOGGNINGEN
        public IQueryable<Errand> GetErrandByInvestigator
        {
            get
            {
                // hämtar inloggad användare
                var userName = contextAcc.HttpContext.User.Identity.Name;

                var data = _context.Errands
                            .Where(td => td.EmployeeId == userName)
                            .Include(td => td.Samples)
                            .Include(td => td.Pictures)
                            .Include(td => td.Department)
                            .Include(td => td.Employee)
                            .Include(td => td.Status);

                return data;
            }
        }


        // tidigare kod
        public IQueryable<Picture> Pictures => _context.Pictures;
        public IQueryable<Sample> Samples => _context.Samples;

        public IQueryable<Employee> Employees => _context.Employees.OrderBy(td => td.Name);

        public IQueryable<Status> Statuses => _context.Status1;

        public IQueryable<Department> DepartmentList => _context.Departments.OrderBy(td => td.DepartmentName);

        public IQueryable<Sequence> Sequences => _context.Sequences;

    


        // i min kod är RefNumber id-parameter och inte integern id i pocoklassen. 
        // Annars kan jag inte få detaljvyn att se exakt likadan ut som er iom
        // att jag har en view component som hämtar all data. RefNumber skrivs ut innan
        // denna komponent i vyn och därför kan jag enbart komma åt den om jag har med den som 
        // asp-route-id
        public Task<Errand> GetErrand(string id)
        {
            return Task.Run(() => Errands
                                .Where(td => td.RefNumber == id)
                                .First());
        }

        public Task<IQueryable<Errand>> GetErrandByInvestigatorAsync()
        {
            // hämtar inloggad användare
            var userName = contextAcc.HttpContext.User.Identity.Name;

            return Task.Run(() => Errands
                        .Where(td => td.EmployeeId == userName)

                );
        }

        public void SaveErrand(Errand errand)
        {
            if (errand.ErrandID == 0)
            {
                // hämtar det första och enda värdet i Sequences
                var currentSeq = _context.Sequences.Where(seq => seq.Id == 1).First();

                // sätter prop RefNumber och dep
                errand.RefNumber = $"{DateTime.Now.ToString("yyyy")} - 45 - {currentSeq.CurrentValue}";
                errand.StatusId = "S_A";

                // lägger till ärende i databasen och sparar
                _context.Errands.Add(errand);
                currentSeq.CurrentValue++;
                _context.SaveChanges();
            }
        }

        // uppdaterar ärende med nytt departmentId
        public void UpdateErrandDepartment(string id, string DepId)
        {
            var errand = _context.Errands.Where(td => td.RefNumber == id).First();

            errand.DepartmentId = DepId;
            _context.SaveChanges();
        }

        // uppdaterar ett ärende med handläggare eller kommentar
        // det mesta av valideringen är gjord med javascript, se vyn CrimeManager, rad 42
        public void UpdateErrandEmployee(string errandId, string commentOrEmployeeId, bool noAction)
        {
            // 1 hämta ärende 
            var errand = _context.Errands.Where(td => td.RefNumber == errandId).First();

            //2 kolla om det är kommentar eller handläggare
            if (noAction)
            {
                // spara kommentar under Errand.InvestigatorInfo, sätt status till S_B
                errand.StatusId = "S_B";
                errand.InvestigatorInfo = commentOrEmployeeId;
            }
            else
            {
                // uppdatera handläggare i Errand.EmployeeId
                errand.EmployeeId = commentOrEmployeeId;
            }
            // 3 spara i databasen
            _context.SaveChanges();
        }


        // sparar filnamn i databasen, i tabellerna Pictures resp Samples
        public void SaveFileToDb(string FileName, bool isImage, string errandId)
        {
            var errand = Errands.Where(td => td.RefNumber == errandId).First();

            // kollar om bild är med    
            if (isImage)
            {
                Picture picture = new Picture { PictureName = FileName, ErrandId = errand.ErrandID };
                _context.Pictures.Add(picture);
            }
            // om ej bild är det en fil
            else if (!isImage)
            {
                Sample sample = new Sample { SampleName = FileName, ErrandId = errand.ErrandID };
                _context.Samples.Add(sample);
            }
            _context.SaveChanges();
        }


        // sparar kommentar från formulär i db.
        public void SaveComments(Errand errand, string errandId)
        {
            // hämtar ärendet i fråga
            var errandToUpdate = _context.Errands.Where(td => td.RefNumber == errandId).First();
            // information i dess nuvurande state
            var currentInfo = errandToUpdate.InvestigatorInfo;
            // action (åtgärd) i dess nuvurande state
            var currentAction = errandToUpdate.InvestigatorAction;

            // om inkommande objekt innehåller åtgärd eller information, concateneras det på datan i databasen
            if (errand.InvestigatorInfo != null)
            {
                currentInfo += " " + errand.InvestigatorInfo;
                errandToUpdate.InvestigatorInfo = currentInfo;
            }
            if (errand.InvestigatorAction != null)
            {
                currentAction += " " + errand.InvestigatorAction;
                errandToUpdate.InvestigatorAction = currentAction;
            }
            // om giltigt val från statusdropdownen är valt uppdateras status i db
            if (!errand.StatusId.Equals("Välj"))
            {
                errandToUpdate.StatusId = errand.StatusId;
            }
            // sparar allt ovan i db
            _context.SaveChanges();
        }


    }
}
