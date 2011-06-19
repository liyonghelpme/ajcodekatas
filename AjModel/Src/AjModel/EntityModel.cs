using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;

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

        internal void AddPropertyModel(PropertyModel model)
        {
            this.properties.Add(model);
        }

        internal void ReplacePropertyModel(PropertyModel model)
        {
            int k;

            for (k = 0; k < this.properties.Count; k++)
                if (this.properties[k].Name == model.Name)
                    break;

            this.properties.RemoveAt(k);
            this.properties.Insert(k, model);
        }
    }
    
    public class EntityModel<T> : EntityModel
    {
        public EntityModel()
            : base(typeof(T))
        {
        }

        public PropertyModel<T, R> GetPropertyModel<R>(Expression<Func<T, R>> expr)
        {
            PropertyInfo info = expr.ToPropertyInfo();
            PropertyModel model = this.GetPropertyModel(info.Name);

            if (model == null)
            {
                PropertyModel<T, R> newmodel = new PropertyModel<T, R>(expr);
                this.AddPropertyModel(newmodel);
                return newmodel;
            }

            PropertyModel<T, R> typedModel = model as PropertyModel<T, R>;

            if (typedModel == null)
            {
                PropertyModel<T, R> newmodel = new PropertyModel<T, R>(expr, model);
                this.ReplacePropertyModel(newmodel);
                return newmodel;
            }

            return typedModel;
        }
    }
}

