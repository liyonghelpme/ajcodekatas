namespace AjCat.Expressions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AjCat.Compiler;

    public class EvalExpression : Expression
    {
        private static EvalExpression instance = new EvalExpression();

        private EvalExpression()
        {
        }

        public static EvalExpression Instance
        {
            get
            {
                return instance;
            }
        }

        public override void Evaluate(Machine machine)
        {
            string function = (string) machine.Pop();
            IList list = (IList)machine.Pop();

            Compiler compiler = new Compiler(function);

            Expression expression = compiler.CompileExpression();

            Machine newmachine = new Machine();

            foreach (object obj in list) 
            {
                newmachine.Push(obj);
            }

            expression.Evaluate(newmachine);

            IList result = new ArrayList(newmachine.StackContent);

            machine.Push(result);
        }

        public override string ToString()
        {
            return "eval";
        }
    }
}
