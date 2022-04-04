using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EXresturant.Models
{
    public class Menukaart
    {
        [Key]
        public int Id { get; set; }


        [Required]
        [Display(Name = "Menukaar Naam")]
        public string Naam { get; set; }




    }
}
