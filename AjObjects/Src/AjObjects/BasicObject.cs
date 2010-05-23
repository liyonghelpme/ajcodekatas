namespace AjObjects
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class BasicObject : DictionaryBase, ICloneable
    {
        private IList<string> names = new List<string>();

        public ICollection<string> Names { get { return this.names; } }

        public Guid Id { get { return (Guid)this["_id"]; } set { this["_id"] = value; } }

        public object this[string name]
        {
            get
            {
                if (!this.Dictionary.Contains(name))
                    return null;

                return this.Dictionary[name];
            }

            set
            {
                if (value == null)
                {
                    if (this.Dictionary.Contains(name))
                    {
                        this.Dictionary.Remove(name);
                        this.names.Remove(name);
                    }

                    return;
                }

                if (!this.Dictionary.Contains(name))
                    this.names.Add(name);

                this.Dictionary[name] = value;
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

        public void Add(String key, Object value)
        {
            this[key] = value;
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

                object v1 = this.Dictionary[name];
                object v2 = bobj.Dictionary[name];

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
                value = HashUtilities.CombineHash(value, HashUtilities.Hash(this.Dictionary[name]));

            return value;
        }

        public BasicObject MakeDeepCopy()
        {
            BasicObject newobj = new BasicObject();

            foreach (string name in this.names)
            {
                object value = this.Dictionary[name];

                if (value is ICloneable)
                    value = ((ICloneable)value).Clone();

                newobj[name] = value;
            }

            return newobj;
        }

        public object Clone()
        {
            return this.MakeDeepCopy();
        }
    }
}
