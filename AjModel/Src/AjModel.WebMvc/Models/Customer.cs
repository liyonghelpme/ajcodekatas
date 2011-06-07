using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AjModel.WebMvc.Models
{
    public class Customer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Notes { get; set; }
    }
}
