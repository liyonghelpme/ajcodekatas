using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace AjModel
{
    public class EntityModel
    {
        private Type type;
        private IList<PropertyModel> properties;

        public EntityModel(Type type)
        {
            this.type = type;

            this.properties = new List<PropertyModel>();

            foreach (var info in this.type.GetProperties())
                this.properties.Add(new PropertyModel(info));

            this.Name = this.type.Name;
        }

        public string Name { get; set; }

        public string SetName { get; set; }

        public string Descriptor { get; set; }

        public string SetDescriptor { get; set; }

        public Type Type
        {
            get
            {
                return this.type;
            }
        }

        public IEnumerable<PropertyModel> Properties
        {
            get
            {
                return this.properties;
            }
        }

        public string GetDescriptor()
        {
            if (this.Descriptor == null)
                return this.Name;

            return this.Descriptor;
        }

        public PropertyModel GetPropertyModel(string name)
        {
            return this.properties.Where(p => p.Name == name).FirstOrDefault();
        }
    }
    
    public class EntityModel<T> : EntityModel
    {
        public EntityModel()
            : base(typeof(T))
        {
        }

        public PropertyModel GetPropertyModel<R>(Expression<Func<T, R>> expr)
        {
            throw new NotImplementedException();
        }
    }
}

