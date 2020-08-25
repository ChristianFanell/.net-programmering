using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LabbUppgift3.Models
{
    // poco klass för status
    public class Status
    {
        [Key]
        public string StatusId { get; set; }
        public string StatusName { get; set; }
    }
}
