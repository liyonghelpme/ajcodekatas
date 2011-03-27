namespace AjAla.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface IContext
    {
        void SetValue(string name, object value);

        object GetValue(string name);

        void SetVariable(string name, object value);
    }
}
