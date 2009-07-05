namespace AjClipper.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class CompositeCommand : BaseCommand
    {
        private List<ICommand> commands = new List<ICommand>();

        public CompositeCommand()
        {
        }

        public void AddCommand(ICommand command)
        {
            this.commands.Add(command);
        }

        public override void Execute(Machine machine, ValueEnvironment environment)
        {
            foreach (ICommand command in commands)
                command.Execute(machine, environment);
        }
    }
}
