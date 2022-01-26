using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace Offers.Models
{
    public class JobsViewModel
    {
        public string JobTitle { get; set; }
        public IEnumerable<ApplyForJob> items { get; set; }
       

    }
}