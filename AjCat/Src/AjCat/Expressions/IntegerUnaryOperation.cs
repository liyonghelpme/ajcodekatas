namespace AjCat.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public abstract class IntegerUnaryOperation : Expression
    {
        public abstract int Apply(int operand);

        public override void Evaluate(Machine machine)
        {
            int op = (int) machine.Pop();

            machine.Push(this.Apply(op));
        }
    }
}
