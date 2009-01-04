namespace AjCat.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    using AjCat.Compiler;

    public class LoadExpression : Expression
    {
        private static LoadExpression instance = new LoadExpression();

        private LoadExpression()
        {
        }

        public static LoadExpression Instance
        {
            get
            {
                return instance;
            }
        }

        public override void Evaluate(Machine machine)
        {
            string filename = (string)machine.Pop();

            Compiler compiler = new Compiler(File.OpenText(filename));

            Expression expression = compiler.CompileExpression();

            expression.Evaluate(machine);
        }

        public override string ToString()
        {
            return "#load";
        }
    }
}
