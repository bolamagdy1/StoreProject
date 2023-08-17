using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StoreProject.Models
{
    public class Product
    {
        [Key]
        public int Product_Id { get; set; }
        [Required]
        [MinLength(5)]
        [Display(Name = "Product Name")]
        public string Product_Name { get; set; }
        [Required]
        [Range(1,1000)]
        [Display(Name = "Product Count")]
        public int Count { get; set; }

        //Relations
        [ForeignKey("Space")]
        public int Space_Id { get; set; }
        public Space Space { get; set; }
    }
}
