using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LabbUppgift3.Models
{
    public class Picture
    {
        // i uppgift 4

        public int PictureId { get; set; }
        public string PictureName { get; set; }
        public int ErrandId { get; set; }


        [ForeignKey("ErrandId")]
        public virtual Errand Errand { get; set; }

    }
}
