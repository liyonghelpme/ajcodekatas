namespace AjCat.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public abstract class DoubleUnaryOperation : Expression
    {
        public abstract double Apply(double operand);

        public override void Evaluate(Machine machine)
        {
            double op = (double) machine.Pop();

            machine.Push(this.Apply(op));
        }
    }
}
