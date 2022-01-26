using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Offers.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [Display(Name =" Job Type")]
       public string CategoryName { get; set; }

        [Required]
        [Display(Name = "Job Description ")]
        public string CategoryDescription { get; set; }

        public virtual ICollection<Jobs> jobs { get; set; }
    }
}