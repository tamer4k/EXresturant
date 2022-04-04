using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EXresturant.Models
{
    public class Bestelling
    {
        public int Id { get; set; }
        public int Tafelnummer { get; set; }
        public bool IsGereed { get; set; }
        public bool IsBetaald { get; set; }
        public List<BestellingItem> Items { get; set; }
        public DateTime DatumBesteld { get; set; }


        //This should be in ViewModel
        
        public int NumberOfItems
        {
            get => Items.Count;
        }

        public Bestelling()
        {
            Items = new List<BestellingItem>();
        }
    }
}

