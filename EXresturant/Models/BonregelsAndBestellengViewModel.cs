using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EXresturant.Models
{
    public class BonregelsAndBestellengViewModel
    {
        public BonBewaren BonBewaren { get; set; }


        public IEnumerable<Product> ProductsList { get; set; }
        public IEnumerable<Bestelling> BestellingslingsList { get; set; }
        public IEnumerable<BestellingItem> BestellingItemslingsList { get; set; }
        public IEnumerable<Reservering> ReserveringsList { get; set; }
    }
}






