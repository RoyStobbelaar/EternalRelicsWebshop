using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ServerEternalRelics.Models
{
    public class Offer
    {
        [Key]
        public int OfferID { get; set; }

        [Required]
        [Display(Name = "Price")]
        public decimal OfferPrice { get; set; }

        [Required]
        [Display(Name = "From")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DateFrom { get; set; }

        [Required]
        [Display(Name = "Till")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DateTill { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 2)]
        [Display(Name = "Offer text")]
        public string OfferText { get; set; }

        public string OfferDate
        {
            get
            {
                return String.Format("{0:dd/MM/yyyy} till {1:dd/MM/yyyy}", DateFrom, DateTill);
            }
        }

        public int ProductID { get; set; }

        public virtual Product Product { get; set; }
    }
}