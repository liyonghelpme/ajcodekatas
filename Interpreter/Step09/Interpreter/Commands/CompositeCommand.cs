﻿namespace Interpreter.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class CompositeCommand : ICommand
    {
        private ICollection<ICommand> commands;

        public CompositeCommand(ICollection<ICommand> commands)
        {
            this.commands = commands;
        }

        public ICollection<ICommand> Commands { get { return this.commands; } }

        public void Execute(BindingEnvironment environment)
        {
            foreach (ICommand command in this.commands)
                command.Execute(environment);
        }
    }
}
