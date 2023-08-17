using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
namespace StoreProject.Models
{
    public class Store
    {
        [Key]
        public int Store_Id { get; set; }
        [Required]
        [MinLength(5)]
        [Display(Name ="Store Name")]
        public string Store_Name { get; set; }
        [Display(Name = "Is Main")]
        public bool IsMain { get; set; }
        [Display(Name = "Is Invoice Direct")]
        public bool IsInvoiceDirect { get; set; }
        [Required]
        [MinLength(5)]
        [Display(Name = "Address")]
        public string Address { get; set; }

        //Relations
        public List<Space> Spaces { get; set; }
    }
}
