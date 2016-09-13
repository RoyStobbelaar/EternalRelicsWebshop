using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ServerEternalRelics.Models
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }

        public string Image { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Description { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}