using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ServerEternalRelics.Models
{
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }

        [Required(ErrorMessage = "Your first name is required.")]
        [StringLength(50, MinimumLength = 2)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Your last name is required.")]
        [StringLength(50, MinimumLength = 2)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Your address is required.")]
        [RegularExpression(@"^([1-9][e][\s])*([a-zA-Z]+(([\.][\s])|([\s]))?)+[1-9][0-9]*(([-][1-9][0-9]*)|([\s]?[a-zA-Z]+))?$", ErrorMessage = "Your address is invalid.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Your zipcode is required.")]
        [RegularExpression(@"^[1-9][0-9]{3}[\s]?[A-Za-z]{2}$", ErrorMessage = "Your zipcode is invalid.")]
        public string Zipcode { get; set; }

        [Required(ErrorMessage = "Your residence is required.")]
        [StringLength(50, MinimumLength = 2)]
        public string Residence { get; set; }

        [Required(ErrorMessage = "Your email address is required.")]
        [RegularExpression(@"^[a-z|A-Z][a-z|A-Z|0-9|]*([_][a-z|A-Z|0-9]+)*([.][a-z|A-Z|0-9]+([_][a-z|A-Z|0-9]+)*)?@[a-z][a-z|A-Z|0-9|]*\.([a-z|A-Z][a-z|A-Z|0-9]*(\.[a-z|A-Z][a-z|A-Z|0-9]*)?)$", ErrorMessage = "Your email address is invalid.")]
        [Display(Name = "E-mail adres")]
        public string EmailAdres { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        [Display(Name = "Name")]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
    }
}