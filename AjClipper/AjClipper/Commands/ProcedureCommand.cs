namespace AjClipper.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjClipper.Language;

    public class ProcedureCommand : BaseCommand
    {
        private string name;
        private List<string> parameterNames;
        private ICommand command;

        public ProcedureCommand(string name, List<string> parameterNames, ICommand command)
        {
            this.name = name;
            this.parameterNames = parameterNames;
            this.command = command;
        }

        public string Name { get { return this.name; } }

        public List<string> ParameterNames { get { return this.parameterNames; } }

        public override void Execute(Machine machine, ValueEnvironment environment)
        {
            Procedure procedure = new Procedure(this.name, this.parameterNames, this.command, machine);
            environment.SetPublicValue(this.name, procedure);
        }
    }
}
