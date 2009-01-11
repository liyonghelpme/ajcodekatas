namespace AjCat.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public abstract class DoubleBinaryOperation : Expression
    {
        public abstract double Apply(double op1, double op2);

        public override void Evaluate(Machine machine)
        {
            double op2 = (double) machine.Pop();
            double op1 = (double) machine.Pop();

            machine.Push(this.Apply(op1, op2));
        }
    }
}
