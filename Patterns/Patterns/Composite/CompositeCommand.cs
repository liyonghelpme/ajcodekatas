using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Patterns.Interpreter;
using Patterns.Visitor;

namespace Patterns.Composite
{
    public class CompositeCommand : ICommand
    {
        private IEnumerable<ICommand> commands;

        public CompositeCommand(IEnumerable<ICommand> commands)
        {
            this.commands = new List<ICommand>(commands);
        }

        public IEnumerable<ICommand> Commands { get { return this.commands; } }

        public void Execute(IContext context)
        {
            foreach (ICommand command in this.commands)
                command.Execute(context);
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
