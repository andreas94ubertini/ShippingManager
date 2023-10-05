using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShippingManager.Models
{
    public class UpdateInfo
    {
        public string State { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int IdShipping {  get; set; }
        public DateTime ExDelivery {  get; set; }
        public string Cf {  get; set; }
        public string PIva { get; set; }
    }
}