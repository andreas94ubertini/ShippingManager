using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShippingManager.Models
{
    public class ShippingDetails
    {
        public Shipping shipping {  get; set; }
        public List<Update> updateList { get; set; }
    }
}