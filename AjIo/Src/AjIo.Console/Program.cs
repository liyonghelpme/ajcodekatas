using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AjIo.Compiler;
using AjIo.Language;

namespace AjIo.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Machine machine = new Machine();
            Parser parser = new Parser(new Lexer(new ConsoleTextReader()));

            for (IMessage message = parser.ParseExpression(); message != null; message = parser.ParseExpression())
            {
                object result = machine.Evaluate(message);

                System.Console.Write("----> ");

                System.Console.WriteLine(Machine.PrintString(result));
            }
        }
    }
}
