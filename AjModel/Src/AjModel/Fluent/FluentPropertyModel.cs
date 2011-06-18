using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AjModel.Fluent
{
    public class FluentPropertyModel<T>
    {
        private PropertyModel model;

        public FluentPropertyModel(PropertyModel model)
        {
            this.model = model;
        }

        public PropertyModel Model
        {
            get
            {
                return this.model;
            }
        }

        public FluentPropertyModel<T> Descriptor(string descriptor)
        {
            this.model.Descriptor = descriptor;
            return this;
        }

        public FluentPropertyModel<T> Description(string description)
        {
            this.model.Description = description;
            return this;
        }
    }
}
