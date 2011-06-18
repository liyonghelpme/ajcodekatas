using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace AjModel
{
    public class PropertyModel
    {
        private PropertyInfo info;

        public PropertyModel(PropertyInfo info)
        {
            this.info = info;
        }

        public string Name { get { return this.info.Name; } }

        public Type Type { get { return this.info.PropertyType; } }

        public string Descriptor { get; set; }

        public string Description { get; set; }

        public object GetValue(object entity)
        {
            return this.info.GetValue(entity, null);
        }
    }
}
