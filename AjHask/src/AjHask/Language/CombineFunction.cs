namespace AjHask.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class CombineFunction : BaseFunction
    {
        private IFunction left;
        private IFunction right;

        public CombineFunction(IFunction left, IFunction right)
        {
            this.left = left;
            this.right = right;
        }

        // TODO review if - 1
        public override int Arity { get { return this.left.Arity - 1; } }

        public override IFunction Apply(IFunction parameter)
        {
            return new CombineFunction(this, parameter);
        }

        public override IFunction Bind(IList<IFunction> parameters)
        {
            IFunction newleft = this.left.Bind(parameters);
            IFunction newright = this.right.Bind(parameters);

            if (newleft.Equals(this.left) && newright.Equals(this.right))
                return this;

            return newleft.Apply(newright);
        }
    }
}
