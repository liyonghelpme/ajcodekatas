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
        private ValueEnvironment environment;

        public Procedure(string name, List<string> parameterNames, ICommand command, Machine machine, ValueEnvironment environment)
        {
            this.name = name;
            this.parameterNames = parameterNames;
            this.command = command;
            this.machine = machine;
            this.environment = environment;
        }

        public object Apply(IList<object> parameters)
        {
            ValueEnvironment normalenv = new ValueEnvironment(this.environment);

            ValueEnvironment localenv = new ValueEnvironment(normalenv, ValueEnvironmentType.Local);

            this.command.Execute(this.machine, localenv);

            return null;
        }
    }
}
