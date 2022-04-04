using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EXresturant.Models
{
    public class BestellingItem
    {
        public int Id { get; set; }

   
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        public int MenukaartId { get; set; }
        [ForeignKey("MenukaartId")]
        public Menukaart Menukaart { get; set; }

        public int BestellingId { get; set; }
        [ForeignKey("BestellingId")]
        public Bestelling Bestelling { get; set; }




        [Required]
        public int Aantal { get; set; }
    }
}
