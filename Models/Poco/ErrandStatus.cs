using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LabbUppgift3.Models
{
    public class ErrandStatus
    {
        [Key]
        public string StatusId { get; set; }
        public string StatusName { get; set; }
    }
}
