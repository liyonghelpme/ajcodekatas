using System;
using System.Collections.Generic;
using System.Text;

namespace AjProlog.Core
{
    public class StringObject : SimpleObject
    {
        private string mValue;

        public StringObject(string v)
        {
            mValue = v;
        }

        public override int GetHashCode()
        {
            return mValue.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is StructureObject)
            {
                obj = ((StructureObject)(obj)).Normalize();
            }
            if (obj == null || !(obj.GetType().Equals(this.GetType())))
            {
                return false;
            }
            return mValue.Equals(((StringObject)(obj)).mValue);
        }

        public override string ToString()
        {
            return mValue;
        }

        public string GetValue()
        {
            return mValue;
        }

        public bool IsVariableName()
        {
            return Utilities.IsVariableName(mValue);
        }
    }
}
