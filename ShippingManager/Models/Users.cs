using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShippingManager.Models
{
    public class Users
    {
        public int IdUser { get; set; }
        [Required(ErrorMessage = "La Username è obbligatoria")]
        public string Username { get; set; }
        [Required(ErrorMessage = "La password è obbligatoria")]
        [Display(Name = "Password")]
        public string Psw {  get; set; }
        public string Role { get; set; }
    }
}