namespace AjLambda
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Pair : Expression
    {
        private Expression left;
        private Expression right;

        public Pair(Expression left, Expression right)
        {
            this.left = left;
            this.right = right;
        }

        public override string ToString()
        {
            string leftText;
            string rightText;

            leftText = this.left.ToString();
            rightText = this.right.ToString();

            if (this.left is Lambda)
                leftText = "(" + leftText + ")";

            if (this.right is Pair)
                rightText = "(" + rightText + ")";

            return leftText + rightText;
        }

        public override Expression Replace(Variable variable, Expression expression)
        {
            Expression newLeft = this.left.Replace(variable, expression);
            Expression newRight = this.right.Replace(variable, expression);

            // Optimization
            if (newLeft == this.left && newRight == this.right)
                return this;

            return new Pair(newLeft, newRight);
        }

        public override Expression Reduce()
        {
            if (this.left is Lambda)
                return ((Lambda)this.left).Apply(this.right);

            Expression newLeft = this.left.Reduce();
            Expression newRight = this.right.Reduce();

            // Optimization
            if (newLeft == this.left && newRight == this.right)
                return this;

            return new Pair(newLeft, newRight);
        }

        public override IEnumerable<Variable> FreeVariables()
        {
            return this.left.FreeVariables().Union(this.right.FreeVariables());
        }
    }
}
