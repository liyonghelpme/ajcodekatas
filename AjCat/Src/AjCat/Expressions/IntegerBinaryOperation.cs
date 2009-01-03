namespace AjCat.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public abstract class IntegerBinaryOperation : Expression
    {
        public abstract int Apply(int op1, int op2);

        public override void Evaluate(Machine machine)
        {
            int op2 = (int) machine.Pop();
            int op1 = (int) machine.Pop();

            machine.Push(this.Apply(op1, op2));
        }
    }
}
