using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace AjModel.WebMvc.ViewModel
{
    public class EntityListViewModel
    {
        public EntityModel EntityModel { get; set; }

        public IEnumerable Entities { get; set; }
    }
}
