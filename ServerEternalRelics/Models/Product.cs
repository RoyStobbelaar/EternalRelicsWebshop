using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ServerEternalRelics.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        public string Image { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 2)]
        public string Description { get; set; }

        [Range(1, 10000000)]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Offer> Offers { get; set; }

        //Offer price
        public decimal CurrentPrice
        {
            get
            {
                decimal totalPrice = Price;

                if (Offers == null)
                {
                    return Price;
                }
                else
                {
                    foreach (Offer a in Offers)
                    {
                        if (a.OfferPrice < totalPrice)
                        {
                            totalPrice = a.OfferPrice;
                        }
                    }
                    return totalPrice;
                }
            }
        }

        /*
        * String for product detail page; returns something like:
        * -10.00-(crossed)
        * 7.50
        */
        public string OfferPrice
        {
            get
            {
                string totalPrice = "";

                if (Offers != null)
                {

                    if (Offers.Count == 0)
                    {
                        return "€" + Price.ToString();
                    }
                    else
                    {
                        totalPrice = "<strike>€" + Price + "</strike></br>";

                        foreach (Offer a in Offers)
                        {
                            totalPrice += "<b>€" + a.OfferPrice + "</b></br>";
                        }
                    }
                }

                return totalPrice;
            }
        }
    }
}