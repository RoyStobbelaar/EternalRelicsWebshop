using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ServerEternalRelics.Models
{
    public class OrderLine
    {
        [Key]
        public int OrderLineID { get; set; }

        [Range(1, 1000)]
        public int Amount { get; set; }

        public decimal OrderLinePrice
        {
            get
            {
                return Amount * Product.CurrentPrice;
            }
        }

        public int ProductID { get; set; }
        public int OrderID { get; set; }

        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }
    }
}