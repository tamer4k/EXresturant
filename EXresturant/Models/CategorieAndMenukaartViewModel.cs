using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EXresturant.Models
{
    public class CategorieAndMenukaartViewModel
    {
        public IEnumerable<Menukaart> MenukaartsList { get; set; }

        public Categorie Categorie { get; set; }

        public List<string> CategoriesList { get; set; }

        public string StatusMessage { get; set; }
    }
}

