namespace AjProcessor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Properties
    {
        private IDictionary<string, object> properties;
        public object this[string name]
        {
            get
            {
                if (this.properties == null || !this.properties.ContainsKey(name))
                    return null;

                return this.properties[name];
            }

            set
            {
                if (this.properties == null)
                    this.properties = new Dictionary<string, object>();

                this.properties[name] = value;
            }
        }
    }
}
