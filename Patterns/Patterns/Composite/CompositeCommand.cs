using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Patterns.Interpreter;

namespace Patterns.Composite
{
    public class CompositeCommand : ICommand
    {
        private IEnumerable<ICommand> commands;

        public CompositeCommand(IEnumerable<ICommand> commands)
        {
            this.commands = new List<ICommand>(commands);
        }

        public void Execute(Context context)
        {
            foreach (ICommand command in this.commands)
                command.Execute(context);
        }
    }
}
