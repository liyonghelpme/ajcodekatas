using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Linq.Expressions;

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

        public virtual object GetValue(object entity)
        {
            return this.info.GetValue(entity, null);
        }

        public bool IsEnumerable()
        {
            if (this.info.PropertyType.GetInterface("IEnumerable") != null)
                return true;

            return false;
        }
    }

    public class PropertyModel<T, P> : PropertyModel
    {
        private Func<T, P> getter;

        public PropertyModel(Expression<Func<T, P>> expr)
            : base(expr.ToPropertyInfo())
        {
            this.getter = expr.Compile();
        }

        public PropertyModel(Expression<Func<T, P>> expr, PropertyModel originalModel)
            : this(expr)
        {
            this.Description = originalModel.Description;
            this.Descriptor = originalModel.Descriptor;
        }

        public override object GetValue(object entity)
        {
            return this.getter((T) entity);
        }
    }
}
