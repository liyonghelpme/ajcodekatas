namespace AjSimpleData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Item
    {
        private Domain domain;
        private object id;
        private Dictionary<string, object> values;
        private bool sharedValues;

        internal Item(object id, Domain domain)
        {
            if (id == null)
                throw new ArgumentNullException("id");

            this.id = id;
            this.domain = domain;
            this.values = new Dictionary<string, object>();
        }

        internal Item(object id, Dictionary<string, object> values)
        {
            this.id = id;
            this.values = values;
            this.sharedValues = true;
        }

        public object Id { get { return this.id; } }

        public Domain Domain { get { return this.domain; } }

        public ICollection<string> Names { get { return this.values.Keys; } }

        public void SetValue(string name, object value)
        {
            ValidateValue(value);

            lock (this.Domain)
            {
                lock (this)
                {
                    if (this.sharedValues)
                        this.CloneValues();

                    this.values[name] = value;
                }
            }
        }

        public object GetValue(string name)
        {
            lock (this)
            {
                if (!this.values.ContainsKey(name))
                    return null;

                return this.values[name];
            }
        }

        public IList<object> GetValues(string name)
        {
            if (!this.values.ContainsKey(name))
                return null;

            lock (this)
            {
                object value = this.values[name];

                if (value is IList<object>)
                    return ((IList<object>)value);

                IList<object> values = new List<object>();

                values.Add(value);

                return values;
            }
        }

        public void RemoveValue(string name)
        {
            if (!this.values.ContainsKey(name))
                return;

            lock (this.Domain)
            {
                lock (this)
                {
                    if (this.sharedValues)
                        this.CloneValues();

                    this.values.Remove(name);
                }
            }
        }

        public void RemoveValue(string name, object value)
        {
            if (!this.values.ContainsKey(name))
                return;

            lock (this.Domain)
            {
                lock (this)
                {
                    object v = this.values[name];

                    if (v.Equals(value))
                    {
                        if (this.sharedValues)
                            this.CloneValues();

                        this.values.Remove(name);
                        return;
                    }

                    if (!(v is IList<object>))
                        return;

                    IList<object> list = (IList<object>)v;

                    if (list.Contains(value))
                    {
                        if (this.sharedValues)
                        {
                            this.CloneValues();
                            list = new List<object>(list);
                            this.values[name] = list;
                        }

                        list.Remove(value);
                    }
                }
            }
        }

        public void AddValue(string name, object value)
        {
            lock (this.Domain)
            {
                lock (this)
                {
                    if (!this.values.ContainsKey(name))
                    {
                        this.SetValue(name, value);
                        return;
                    }

                    object oldvalue = this.values[name];

                    IList<object> list;

                    if (oldvalue is IList<object>)
                        list = (IList<object>)oldvalue;
                    else
                    {
                        list = new List<object>();
                        list.Add(oldvalue);
                        if (this.sharedValues)
                            this.CloneValues();
                        this.values[name] = list;
                    }

                    if (this.sharedValues)
                    {
                        this.CloneValues();
                        list = new List<object>(list);
                        this.values[name] = list;
                    }

                    list.Add(value);
                }
            }
        }

        public Item CloneItem()
        {
            this.sharedValues = true;
            return new Item(this.id, this.values);
        }

        private void ValidateValue(object value)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            if (value is String || value is System.ValueType)
                return;

            throw new InvalidOperationException(string.Format("Invalid value type {0}", value.GetType().Name));
        }

        private void CloneValues()
        {
            this.values = new Dictionary<string, object>(this.values);
            this.sharedValues = false;
        }
    }
}
