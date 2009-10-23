namespace AjHask.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class CombineFunction : IFunction
    {
        private IFunction left;
        private IFunction right;

        public CombineFunction(IFunction left, IFunction right)
        {
            this.left = left;
            this.right = right;
        }

        // TODO review if - 1
        public int Arity { get { return this.left.Arity - 1; } }

        public object Value { get { return this; } }

        public IFunction Apply(IFunction parameter)
        {
            IFunction newleft = this.left.Apply(parameter);
            IFunction newright = this.right.Apply(parameter);

            if (newleft.Equals(this.left) && newright.Equals(this.right))
                return this;

            return newleft.Apply(newright);
        }
    }
}
