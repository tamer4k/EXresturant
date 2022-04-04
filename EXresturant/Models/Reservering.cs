using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EXresturant.Models
{
    using System;  
    using System.Collections.Generic;
    public class Reservering
    {

        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "Naam is verplicht")]
        [Display(Name = "Klant Name")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Tafelnummer is verplicht")]
        [Display(Name = "Tafel nummer")]
        public int Tafelnummer { get; set; }


        [Required(ErrorMessage = "PhoneNummer is verplicht")]
        [Display(Name = "PhoneNummer")]
        [StringLength(10)]
        public string PhoneNummer { get; set; }


        [Required(ErrorMessage = "Email is verplicht")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Vul geldige Email in")]

        public string Email { get; set; }


        [Required(ErrorMessage = "Aantal is verplicht")]
        [Display(Name = "Aantal persoon")]
        public int Aantalpersoon { get; set; }


        [DataType(DataType.Date)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy hh:mm:ss}")]
        public DateTime Datum { get; set; }


        [DataType(DataType.Time)]
        public DateTime BeginTijd { get; set; }

        [ScaffoldColumn(false)]
        public DateTime DatumAangemak { get; set; } = DateTime.Now;


        public bool IsGekomen { get; set; }

        public virtual ICollection<Bestelling> Bestellings { get; set; }


    }
}
