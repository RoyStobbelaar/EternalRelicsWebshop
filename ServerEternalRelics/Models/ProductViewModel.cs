using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ServerEternalRelics.Models
{
    /// <summary>
    /// Not really used anymore
    /// </summary>
    public class ProductViewModel
    {
        public Product Product { get; set; }

        public ProductViewModel(Product product)
        {
            this.Product = product;
            this.Product.Offers = new List<Offer>();
        }

        /*
         * Returns altered prijs from product
         */
        [DataType(DataType.Currency)]
        public decimal Price
        {
            get
            {
                decimal totalPrice = Product.Price;

                if (Product.Offers.Count == 0)
                {
                    return Product.Price;
                }
                else
                {
                    foreach (Offer a in Product.Offers)
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

                if (Product.Offers.Count == 0)
                {
                    return "€" + Product.Price.ToString();
                }
                else
                {
                    totalPrice = "<strike>€" + Product.Price + "</strike></br>";

                    foreach (Offer a in Product.Offers)
                    {
                        totalPrice += "<b>€" + a.OfferPrice + "</b></br>";
                    }
                }

                return totalPrice;
            }
        }
    }
}