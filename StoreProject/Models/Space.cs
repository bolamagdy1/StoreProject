using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StoreProject.Models
{
    public class Space
    {
        [Key]
        public int Space_Id { get; set; }
        [Required]
        [MinLength(5)]
        [Display(Name = "Space Name")]
        public string Space_Name { get; set; } = "Default Space";

        //Relations
        [ForeignKey("Store")]
        public int Store_Id { get; set; }
        public Store Store { get; set; }

        public List<Product> Products { get; set; }
    }
}
