using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShippingManager.Models
{
    public class Update
    {
        public int IdUpdate { get; set; }
        [Required(ErrorMessage = "Campo obbligatorio")]
        [Display(Name = "Stato Spedizione")]
        public string State { get; set; }
        [Required(ErrorMessage = "Campo obbligatorio")]
        [Display(Name = "Luogo in cui si trova")]
        public string Location { get; set; }
        [Required(ErrorMessage = "Campo obbligatorio")]
        [Display(Name = "Descrizione")]
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int IdShipping { get; set; }
    }
}