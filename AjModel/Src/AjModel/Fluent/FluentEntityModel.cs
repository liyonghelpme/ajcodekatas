using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AjModel.Fluent
{
    public class FluentEntityModel<T>
    {
        private EntityModel model;

        public FluentEntityModel(EntityModel model)
        {
            this.model = model;
        }

        public EntityModel Model
        {
            get
            {
                return this.model;
            }
        }

        public FluentEntityModel<T> Name(string name)
        {
            this.model.Name = name;
            return this;
        }

        public FluentEntityModel<T> SetName(string setName)
        {
            this.model.SetName = setName;
            return this;
        }

        public FluentEntityModel<T> Descriptor(string descriptor)
        {
            this.model.Descriptor = descriptor;
            return this;
        }

        public FluentEntityModel<T> SetDescriptor(string setDescriptor)
        {
            this.model.SetDescriptor = setDescriptor;
            return this;
        }
    }
}
