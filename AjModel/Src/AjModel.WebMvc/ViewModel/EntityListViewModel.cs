using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace AjModel.WebMvc.ViewModel
{
    public class EntityListViewModel
    {
        public string Title
        {
            get
            {
                return this.EntityModel.SetName == null ? string.Format("{0} List", this.EntityModel.Name) : this.EntityModel.SetName;
            }
        }

        public EntityModel EntityModel { get; set; }

        public IEnumerable Entities { get; set; }
    }
}

