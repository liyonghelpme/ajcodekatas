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
            Parser parser = new Parser(new Lexer(System.Console.In));

            for (object expression = parser.ParseExpression(); expression != null; expression = parser.ParseExpression())
            {
                object result = machine.Evaluate(expression);
                if (result != null)
                    System.Console.WriteLine(result.ToString());
                else
                    System.Console.WriteLine("null");
            }
        }
    }
}
