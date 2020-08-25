using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LabbUppgift3.Models
{

    // poco klass för avdelning
    public class Department
    {
        [Key]
        public string DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }
}
