using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabbUppgift3.Models
{
    public class Errand
    {

        // modell med formulärvalidering för ärenden

            // ändrad från string till int
        [Key]
        public int ErrandID {get; set;}

        // detta är ErrandID från uppgift 1 och 2, ovan
        public string RefNumber { get; set; }

        // obligatoriskt fält i formulär
        [Required(ErrorMessage = "Du måste ange plats för din observation")]
        public string Place { get; set; }

        [Required(ErrorMessage = "Du måste ange type av brott")]
        public string TypeOfCrime { get; set; }

        // obligatoriskt, datum
        [Required(ErrorMessage = "Datum är obligatoriskt")]
        [UIHint("Date")]
        public DateTime DateOfObservation { get; set; }

        [UIHint("Text")]
        public string Observation { get; set; }

        public string InvestigatorInfo { get; set; }

        public string InvestigatorAction { get; set; }


        [Required(ErrorMessage = "Du måste fylla i ditt namn")]
        public string InformerName { get; set; }

        // obligatoriskt, enbart svenska nummer
        [Required(ErrorMessage ="Du måste fylla i ditt telefonnummer")]
        [UIHint("PhoneNumber")]
        [RegularExpression(@"\d{2,4}-\d{5,9}", ErrorMessage = "Du måste fylla i ett giltigt telefonnummer")]
        public string InformerPhone { get; set; }


       
        public string StatusId { get; set; }
 
        [ForeignKey("StatusId")]
        public virtual Status Status { get; set; }

        public string DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        public virtual Department Department { get; set; }

        public string EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }


        // till uppgift 4:

        public ICollection<Sample> Samples { get; set; }

        public ICollection<Picture> Pictures { get; set; }


    }
}
