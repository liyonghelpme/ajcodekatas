using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace AjModel.Fluent
{
    public class FluentEntityModel<T> where T : new()
    {
        private EntityModel<T> model;

        public FluentEntityModel(EntityModel<T> model)
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

        public FluentEntityModel<T> Property<R>(Expression<Func<T, R>> expr, Action<FluentPropertyModel<T, R>> action)
        {
            var propertyModel = new FluentPropertyModel<T, R>(this.model.GetPropertyModel(expr));
            action(propertyModel);
            return this;
        }

        public FluentEntityModel<T> SetDescriptor(string setDescriptor)
        {
            this.model.SetDescriptor = setDescriptor;
            return this;
        }
    }
}
