using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public ICollection<PropertyModel> Properties
        {
            get
            {
                return this.properties;
            }
        }
    }
}

