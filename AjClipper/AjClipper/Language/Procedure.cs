namespace AjClipper.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AjClipper.Commands;

    public class Procedure
    {
        private string name;
        private IList<string> parameterNames;
        private ICommand command;
        private Machine machine;

        public Procedure(string name, IList<string> parameterNames, ICommand command, Machine machine)
        {
            this.name = name;
            this.parameterNames = parameterNames;
            this.command = command;
            this.machine = machine;
        }

        public object Apply(IList<object> parameters, ValueEnvironment environment)
        {
            ValueEnvironment normalenv = new ValueEnvironment(environment);

            if (this.parameterNames != null)
                for (int k = 0; k < this.parameterNames.Count; k++)
                    normalenv.SetValue(this.parameterNames[k], parameters[k]);

            ValueEnvironment localenv = new ValueEnvironment(normalenv, ValueEnvironmentType.Local);

            try
            {
                this.command.Execute(this.machine, localenv);
            }
            catch (ReturnException ex)
            {
                return ex.Value;
            }

            return null;
        }
    }
}
