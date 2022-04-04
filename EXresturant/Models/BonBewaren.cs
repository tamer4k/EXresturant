using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EXresturant.Models
{
    public class BonBewaren
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public string KlantName { get; set; }

        public int Tafelnummer { get; set; }

        public string ProductName { get; set; }

        public double Prijs { get; set; }

        public int Aantal { get; set; }

        public DateTime DatumBesteld { get; set; }

        public bool IsBetaald { get; set; }



    }
}
