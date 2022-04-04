using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EXresturant.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }


        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString ="{0:c}")]
        public decimal Price { get; set; }



        public string Image { get; set; }


        [Display(Name = "Menukaart")]
        public int MenukaartId { get; set; }
        [ForeignKey("MenukaartId")]
        public virtual Menukaart Menukaart { get; set; }



        [Display(Name = "Categorie")]
        public int CategorieId { get; set; }
        [ForeignKey("CategorieId")]
        public virtual Categorie Categorie { get; set; }









    }
}
