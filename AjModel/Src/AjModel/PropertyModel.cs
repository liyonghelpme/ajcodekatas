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

        public string Name
        {
            get
            {
                return this.info.Name;
            }
        }

        public object GetValue(object entity)
        {
            return this.info.GetValue(entity, null);
        }
    }
}
