namespace AjCat.Expressions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ListExpression : Expression
    {
        public override void Evaluate(Machine machine)
        {
            Expression expression = (Expression)machine.Pop();
            IList result = new ArrayList();

            Machine newmachine = new Machine();

            expression.Evaluate(newmachine);

            while (newmachine.StackCount > 0)
            {
                result.Insert(0, newmachine.Pop());
            }

            machine.Push(result);
        }
    }
}
