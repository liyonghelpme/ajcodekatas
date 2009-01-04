namespace AjCat.Console
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AjCat;
    using AjCat.Compiler;
    using AjCat.Expressions;

    public class Program
    {
        public static void Main(string[] args)
        {
            PrintPrologue();

            Machine machine = new Machine();
            string line;

            while (true)
            {
                try
                {
                    line = GetLine();
                    Compiler compiler = new Compiler(line);
                    Expression expression = compiler.CompileExpression();

                    if (expression == null)
                    {
                        continue;
                    }

                    expression.Evaluate(machine);
                    PrintStack(machine);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private static string GetLine()
        {
            Console.Out.Write(">> ");
            Console.Out.Flush();

            return Console.ReadLine();
        }

        private static void PrintPrologue()
        {
            Console.WriteLine("AjCat 0.1");
            Console.WriteLine("by http://www.ajlopez.com/ http://ajlopez.wordpress.com");
            Console.WriteLine();
        }

        private static void PrintStack(Machine machine)
        {
            object[] content = machine.StackContent;

            if (content == null || content.Length == 0)
            {
                Console.WriteLine("_empty_");
            }

            Console.Write("Stack:");

            for (int k = content.Length; --k >= 0; )
            {
                Console.Write(" ");
                Console.Write(content[k].ToString());
            }

            Console.WriteLine();
        }
    }
}
