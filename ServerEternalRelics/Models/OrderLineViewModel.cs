using ServerEternalRelics.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ServerEternalRelics.Models
{
    public class OrderLineViewModel
    {
        public ProductViewModel Product { get; set; }
        public OrderLine OrderLine { get; set; }
        private WebshopContext db = new WebshopContext();


        public OrderLineViewModel(OrderLine orderLine)
        {
            this.Product = new ProductViewModel(db.Product.Find(orderLine.ProductID));
            this.OrderLine = orderLine;
        }

        public int Amount
        {
            get
            {
                return OrderLine.Amount;
            }
        }

        [DataType(DataType.Currency)]
        public decimal Total
        {
            get
            {
                return OrderLine.Amount * Product.Price;
            }
        }
    }
}