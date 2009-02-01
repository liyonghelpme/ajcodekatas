namespace AjLambda
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Variable : Expression
    {
        private static IEnumerable<string> variableNames = CreateVariableNames();
        private string name;

        public Variable(string name)
        {
            this.name = name;
        }

        public string Name { get { return this.name; } }

        public Variable RenameVariable(IEnumerable<Variable> freeVariables)
        {
            List<string> names = new List<string>();

            names.Add(this.name);

            foreach (Variable v in freeVariables)
                names.Add(v.name);

            string newName = variableNames.Except(names).First();

            return new Variable(newName);
        }

        public override string ToString()
        {
            return this.Name;
        }

        public override Expression Replace(Variable variable, Expression expression)
        {
            if (this == variable || this.Name == variable.Name)
                return expression;

            return this;
        }

        public override Expression Reduce()
        {
            return this;
        }

        public override IEnumerable<Variable> FreeVariables()
        {
            List<Variable> vars = new List<Variable>();
            vars.Add(this);
            return vars;
        }

        private static IEnumerable<string> CreateVariableNames()
        {
            List<string> varNames = new List<string>();

            for (char ch = 'a'; ch <= 'z'; ch++)
                varNames.Add(new string(new char[] { ch }));

            return varNames;
        }
    }
}

