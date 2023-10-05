using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShippingManager.Models
{
    public class SearchInfo
    {
        [Required(ErrorMessage = "Campo obbligatorio")]
        [Display(Name = "Inserisci Cf se sei un privato o P.Iva se sei una azienda")]
        public string CustomerIdentity {  get; set; }
        [Required(ErrorMessage = "Campo obbligatorio")]
        [Display(Name = "Identificativo Spedizione")]
        public int IdShipping { get; set; }
        [Display(Name = "Spunta se sei un azienda")]
        public bool IsAzienda { get; set; }
    }
}