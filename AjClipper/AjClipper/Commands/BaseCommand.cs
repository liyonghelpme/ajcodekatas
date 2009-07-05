namespace AjClipper.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public abstract class BaseCommand : AjClipper.Commands.ICommand
    {
        public abstract void Execute(Machine machine, ValueEnvironment environment);
    }
}
