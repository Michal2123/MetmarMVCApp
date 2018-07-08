
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MetmarMVCApp.Models
{
    public class Order
    {
        public int Count { get; set; }
        public Przedmioty Item { get; set; }
        public DateTime DateCreated { get; set; }
        public decimal ToPay { get; set; }
    }
}