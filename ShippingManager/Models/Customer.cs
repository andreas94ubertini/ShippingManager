using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShippingManager.Models
{
    public class Customer
    {
        public int IdCustomer { get; set; }
        [Required(ErrorMessage = "Campo obbligatorio")]
        [Display(Name = "Nome")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Campo obbligatorio")]
        [Display(Name = "Cognome")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Campo obbligatorio")]
        [Display(Name = "Spunta se rappresenta un azienda")]
        public bool IsAzienda { get; set; }
        [Display(Name = "Codice Fiscale")]
        public string Cf {  get; set; }
        [Display(Name = "PIva (compilare solo se Azienda)")]
        public string PIva { get; set; }
    }
}