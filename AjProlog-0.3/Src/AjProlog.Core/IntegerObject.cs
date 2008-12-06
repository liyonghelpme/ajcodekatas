using System;
using System.Collections.Generic;
using System.Text;

namespace AjProlog.Core
{
    public class IntegerObject : SimpleObject
    {
        private int value;

        public IntegerObject(int value)
        {
            this.value = value;
        }

        public override int GetHashCode()
        {
            return this.value.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !obj.GetType().Equals(this.GetType()))
            {
                return false;
            }

            return this.value == ((IntegerObject)obj).value;
        }

        public override string ToString()
        {
            return this.value.ToString();
        }

        public int GetValue()
        {
            return this.value;
        }

        public override object ToObject()
        {
            return this.GetValue();
        }
    }
}
