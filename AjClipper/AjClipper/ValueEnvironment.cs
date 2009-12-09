namespace AjClipper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ValueEnvironment
    {
        private ValueEnvironmentType type;
        private ValueEnvironment parent;
        private Dictionary<string, object> values = new Dictionary<string, object>();

        public ValueEnvironment()
            : this(ValueEnvironmentType.Normal)
        {
        }

        public ValueEnvironment(ValueEnvironmentType type)
            : this(null, type)
        {
        }

        public ValueEnvironment(ValueEnvironment parent)
            : this(parent, ValueEnvironmentType.Normal)
        {
        }

        public ValueEnvironment(ValueEnvironment parent, ValueEnvironmentType type)
        {
            this.parent = parent;
            this.type = type;
        }

        public ValueEnvironmentType Type { get { return this.type; } }

        public void SetValue(string key, object value)
        {
            if (this.values.ContainsKey(key))
            {
                this.values[key] = value;
                return;
            }

            if (this.type == ValueEnvironmentType.Local)
            {
                this.parent.SetValue(key, value);
                return;
            }

            if (this.type == ValueEnvironmentType.Normal)
            {
                ValueEnvironment pubenv = this.GetPublicEnvironmentContaining(key);

                if (pubenv != null)
                {
                    pubenv.SetValue(key, value);
                    return;
                }
            }

            this.values[key] = value;
        }

        public object GetValue(string key)
        {
            if (!this.values.ContainsKey(key))
            {
                if (this.parent != null)
                    return this.parent.GetValue(key);

                return null;
            }

            return this.values[key];
        }

        public void SetLocalValue(string key, object value)
        {
            if (this.type != ValueEnvironmentType.Local)
                throw new InvalidOperationException("No Local Environment");

            this.values[key] = value;
        }

        public void SetPublicValue(string key, object value)
        {
            if (this.type != ValueEnvironmentType.Public)
                if (this.parent != null)
                {
                    this.parent.SetPublicValue(key, value);
                    return;
                }
                else
                    throw new InvalidOperationException("No Public Environment");

            this.values[key] = value;
        }

        public ValueEnvironment GetNonLocalEnvironment()
        {
            if (this.type != ValueEnvironmentType.Local)
                return this;

            if (this.parent != null)
                return this.parent.GetNonLocalEnvironment();

            return null;
        }

        public ValueEnvironment GetPublicEnvironment()
        {
            if (this.type == ValueEnvironmentType.Public)
                return this;

            if (this.parent != null)
                return this.parent.GetPublicEnvironment();

            throw new InvalidOperationException("No Public Environment");
        }

        public ValueEnvironment GetPublicEnvironmentContaining(string key)
        {
            if (this.type == ValueEnvironmentType.Public && this.values.ContainsKey(key))
                return this;

            if (this.parent != null)
                return this.parent.GetPublicEnvironmentContaining(key);

            return null;
        }
    }

    public enum ValueEnvironmentType
    {
        Normal,
        Public,
        Local
    }
}
