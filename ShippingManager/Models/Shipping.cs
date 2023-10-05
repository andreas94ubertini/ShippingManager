using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShippingManager.Models
{
    public class Shipping
    {
        public int IdShipping {  get; set; }
        [Required(ErrorMessage = "Campo obbligatorio")]
        [Display(Name = "Data di spedizione")]
        public DateTime ShippingDate { get; set; }
        [Required(ErrorMessage = "Campo obbligatorio")]
        [Display(Name = "Peso in kg del pacco")]
        public double Weight { get; set; }
        [Required(ErrorMessage = "Campo obbligatorio")]
        [Display(Name = "Città di destinazione")]
        public string Destination { get; set; }
        [Required(ErrorMessage = "Campo obbligatorio")]
        [Display(Name = "Via di residenza ")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Campo obbligatorio")]
        [Display(Name = "Destinatario")]
        public string Addressee { get; set; }
        [Required(ErrorMessage = "Campo obbligatorio")]
        [Display(Name = "Prezzo")]
        public double Price { get; set; }
        [Required(ErrorMessage = "Campo obbligatorio")]
        [Display(Name = "Data di consegna prevista")]
        public DateTime ExDelivery {  get; set; }
        [Required(ErrorMessage = "Campo obbligatorio")]
        [Display(Name = "Cliente")]
        public int IdCustomer {  get; set; }
        public string State { get; set; }

    }
}