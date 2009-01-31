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
    }
}
