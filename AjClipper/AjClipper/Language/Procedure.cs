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
        private List<string> parameterNames;
        private ICommand command;
        private Machine machine;

        public Procedure(string name, List<string> parameterNames, ICommand command, Machine machine)
        {
            this.name = name;
            this.parameterNames = parameterNames;
            this.command = command;
            this.machine = machine;
        }

        public object Apply(IList<object> parameters, ValueEnvironment environment)
        {
            ValueEnvironment normalenv = new ValueEnvironment(environment);

            ValueEnvironment localenv = new ValueEnvironment(normalenv, ValueEnvironmentType.Local);

            this.command.Execute(this.machine, localenv);

            return null;
        }
    }
}
