using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabbUppgift3.Models
{
    // poco klass för handläggare
    public class Employee
    {
        [Key]
        public string EmployeeId { get; set; }
        public string RoleTitle { get; set; }
        public string DepartmentId { get; set; }
        public string Name { get; set;  }

        [ForeignKey("DepartmentId")]
       public virtual Department Department { get; set; }

        // enbart för checkbox
        [NotMapped]
        public bool NoAction { get; set; }

        [NotMapped]
        public string NotInvestigate { get; set; }
    }
}
