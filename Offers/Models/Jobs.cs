using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Models;
using System.Web.Mvc;

namespace Offers.Models
{
    public class Jobs
    {
        public int Id { get; set; }
        [Display(Name="Job Title")]
        public string JobTitle { get; set; }

        [Display(Name = "Job Description")]
        
        public string JobDescription { get; set; }

        [Display(Name = "Job Image")]
        public string JobImage { get; set; }

        [Display(Name = "Job Type")]
        public int CategoryId { get; set; }

        [AllowHtml]
        [Display(Name = "Job Content ")]
        public string JobContent { get; set; }

        public string UserId { get; set; }

        public virtual Category category { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}