//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace LabbUppgift3.Models
//{

//    // "falsk databas"
//    public class FakeErrendRep : IErrandRep
//    {

//        //skapar en lista med objekt av ärenden
//        public IQueryable<Errand> Errands => new List<Errand>
//        {
//            new Errand{ ErrandID=1, RefNumber = "2018-45-0001", Place = "Skogslunden vid Jensens gård", TypeOfCrime="Sopor", DateOfObservation = new DateTime(2018,04,24), Observation ="Anmälaren var på promeand i skogslunden när hon upptäckte soporna", InvestigatorInfo = "Undersökning har gjorts och bland soporna hittades bl.a ett brev till Gösta Olsson", InvestigatorAction = "Brev har skickats till Gösta Olsson om soporna och anmälan har gjorts till polisen 2018-05-01", InformerName = "Ada Bengtsson", InformerPhone = "0432-5545522", StatusId="Klar", DepartmentId="Renhållning och avfall", EmployeeId ="Susanne Fred"},
//            new Errand{ ErrandID=2, RefNumber = "2018-45-0002", Place = "Småstadsjön", TypeOfCrime="Oljeutsläpp", DateOfObservation = new DateTime(2018,04,29), Observation ="Jag såg en oljefläck på vattnet när jag var där för att fiska", InvestigatorInfo = "Undersökning har gjorts på plats, ingen fläck har hittas", InvestigatorAction = "", InformerName = "Bengt Svensson", InformerPhone = "0432-5152255", StatusId="Ingen åtgärd", DepartmentId="Natur och Skogsvård", EmployeeId ="Oskar Ivarsson"},
//            new Errand{ ErrandID=3, RefNumber = "2018-45-0003", Place = "Ödehuset", TypeOfCrime="Skrot", DateOfObservation = new DateTime(2018,05,02), Observation ="Anmälaren körde förbi ödehuset och upptäcker ett antal bilar och annat skrot", InvestigatorInfo = "Undersökning har gjorts och bilder har tagits", InvestigatorAction = "", InformerName = "Olle Pettersson", InformerPhone = "0432-5255522", StatusId="Påbörjad", DepartmentId="Miljö och Hälsoskydd", EmployeeId ="Lena Larsson"},
//            new Errand{ ErrandID=4, RefNumber = "2018-45-0004", Place = "Restaurang Krögaren", TypeOfCrime="Buller", DateOfObservation = new DateTime(2018,06,04), Observation ="Restaurangen hade för högt ljud på så man inte kunde sova", InvestigatorInfo = "Bullermätning har gjorts. Man håller sig inom riktvärden", InvestigatorAction = "Meddelat restaurangen att tänka på ljudet i fortsättning", InformerName = "Roland Jönsson", InformerPhone = "0432-5322255", StatusId="Klar", DepartmentId="Miljö och Hälsokydd", EmployeeId ="Martin Kvist"},
//            new Errand{ ErrandID=5, RefNumber = "2018-45-0005", Place = "Torget", TypeOfCrime="Klotter", DateOfObservation = new DateTime(2018,07,10), Observation ="Samtliga skräpkorgar och bänkar är nedklottrade", InvestigatorInfo = "", InvestigatorAction = "", InformerName = "Peter Svensson", InformerPhone = "0432-5322555", StatusId="Inrapporterad", DepartmentId="Ej tillsatt", EmployeeId ="Ej tillsatt"}

//        }.AsQueryable<Errand>();


//        // lista med handläggare
//        public IQueryable<Employee> Employees => new List<Employee>
//        {
//            new Employee{Name = "Susanne Fred"},
//            new Employee{Name = "Lena Larsson"},
//            new Employee{Name = "Martin Kvist"},
//            new Employee{Name = "Oskar Ivarsson"},
//            new Employee{Name = "Ej tillsatt"},

//        }.AsQueryable<Employee>();

//        // lista med avdelning
//        public IQueryable<Department> DepartmentList => new List<Department>
//        {
//            new Department {DepartmentName = "Renhållning och avfall"},
//            new Department {DepartmentName = "Natur och skogsvård"},
//            new Department {DepartmentName = "Miljö och hälsoskydd"},
//            new Department {DepartmentName = "Ej tillsatt"},
//        }.AsQueryable<Department>();

//        // lista med status
//        public IQueryable<Status> Statuses => new List<Status>
//        {
//            new Status {StatusName = "Klar"},
//            new Status {StatusName = "Påbörjad"},
//            new Status {StatusName = "Inrapporterad"},
//            new Status {StatusName = "Ingen åtgärd"},

//        }.AsQueryable<Status>();



//        // returnerar objekt baserat på id
//        public async Task<Errand> GetErrand(string id)
//        {
//            return await Task.Run(() => Errands.Where(td => td.RefNumber == id).First());        
//        }
//    }
//}
