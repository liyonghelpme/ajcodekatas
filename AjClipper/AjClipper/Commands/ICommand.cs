namespace AjClipper.Commands
{
    using System;

    public interface ICommand
    {
        void Execute(AjClipper.Machine machine, ValueEnvironment environment);
    }
}
