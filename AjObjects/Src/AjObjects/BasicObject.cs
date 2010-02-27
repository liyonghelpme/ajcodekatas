namespace AjObjects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class BasicObject : ICloneable
    {
        private Dictionary<string, object> values = new Dictionary<string, object>();
        private IList<string> names = new List<string>();

        public ICollection<string> Names { get { return this.names; } }

        public Guid Id { get { return (Guid)this["_id"]; } set { this["_id"] = value; } }

        public object this[string name]
        {
            get
            {
                if (!this.values.ContainsKey(name))
                    return null;

                return this.values[name];
            }

            set
            {
                if (value == null)
                {
                    if (this.values.ContainsKey(name))
                    {
                        this.values.Remove(name);
                        this.names.Remove(name);
                    }

                    return;
                }

                if (!this.values.ContainsKey(name))
                    this.names.Add(name);

                this.values[name] = value;
            }
        }

        public static BasicObject CreateObject(params object[] namevaluepairs)
        {
            if ((namevaluepairs.Length % 2) != 0)
                throw new InvalidOperationException("Odd count of name-values in CreateObject");

            BasicObject obj = new BasicObject();

            for (int k = 0; k < namevaluepairs.Length; k += 2)
                obj[(string)namevaluepairs[k]] = namevaluepairs[k + 1];

            return obj;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is BasicObject))
                return false;

            if (this == obj)
                return true;

            BasicObject bobj = (BasicObject)obj;

            if (this.names.Count != bobj.names.Count)
                return false;

            foreach (string name in this.names)
            {
                if (!bobj.names.Contains(name))
                    return false;

                object v1 = this.values[name];
                object v2 = bobj.values[name];

                if (v1 == null)
                {
                    if (v2 != null)
                        return false;

                    continue;
                }

                if (!v1.Equals(v2))
                    return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            IOrderedEnumerable<string> ordnames = this.names.OrderBy(n => n);

            int value = HashUtilities.CombineHash(ordnames);

            foreach (string name in ordnames)
                value = HashUtilities.CombineHash(value, HashUtilities.Hash(this.values[name]));

            return value;
        }

        public BasicObject MakeDeepCopy()
        {
            BasicObject newobj = new BasicObject();

            foreach (string name in this.names)
            {
                Object value = this.values[name];

                if (value is ICloneable)
                    value = ((ICloneable)value).Clone();

                newobj[name] = value;
            }

            return newobj;
        }

        public Object Clone()
        {
            return this.MakeDeepCopy();
        }
    }
}
