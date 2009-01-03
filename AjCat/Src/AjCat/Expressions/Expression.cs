namespace AjCat.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public abstract class Expression
    {
        public abstract void Evaluate(Machine machine);
    }
}
