using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AjModel.Fluent
{
    public class FluentPropertyModel<T, P>
    {
        private PropertyModel<T, P> model;

        public FluentPropertyModel(PropertyModel<T, P> model)
        {
            this.model = model;
        }

        public PropertyModel<T, P> Model
        {
            get
            {
                return this.model;
            }
        }

        public FluentPropertyModel<T, P> Descriptor(string descriptor)
        {
            this.model.Descriptor = descriptor;
            return this;
        }

        public FluentPropertyModel<T, P> Description(string description)
        {
            this.model.Description = description;
            return this;
        }
    }
}
