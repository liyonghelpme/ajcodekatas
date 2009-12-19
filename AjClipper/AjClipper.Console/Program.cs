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
            Parser parser;

            foreach (string filename in args)
            {
                parser = new Parser(System.IO.File.OpenText(filename));

                ICommand command = parser.ParseCommand();

                command.Execute(machine, machine.Environment);
            }

            parser = new Parser(System.Console.In);

            for (ICommand command = parser.ParseCommand(); command != null; command = parser.ParseCommand())
                command.Execute(machine, machine.Environment);
        }
    }
}
