namespace AjIo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    
    using AjIo.Language;
    using AjIo.Methods.Arithmetic;

    public class Machine : DerivedObject
    {
        public Machine()
            : base(new TopObject())
        {
            this.SetSlot("Object", this.Parent);
            this.SetSlot("List", new ListObject(this.Parent));
        }

        public static string PrintString(object obj)
        {
            if (obj == null)
                return "nil";
            if (obj is string)
                return string.Format("\"{0}\"", obj);
            if (obj is bool)
                return obj.ToString().ToLower();

            return obj.ToString();
        }
    }
}
