namespace AjClipper.Console
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AjClipper;
    using AjClipper.Compiler;
    using AjClipper.Commands;

    class Program
    {
        static void Main(string[] args)
        {
            Machine machine = new Machine();

            foreach (string filename in args)
            {
                Parser parser = new Parser(System.IO.File.OpenText(filename));

                ICommand command = parser.ParseCommand();

                command.Execute(machine, machine.Environment);
            }
        }
    }
}
