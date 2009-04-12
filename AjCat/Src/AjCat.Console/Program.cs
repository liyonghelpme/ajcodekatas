namespace AjCat.Console
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    using AjCat;
    using AjCat.Compiler;
    using AjCat.Expressions;

    public class Program
    {
        private static Machine machine = new Machine();
        private static ExpressionEnvironment environment = new TopExpressionEnvironment();

        public static void Main(string[] args)
        {
            PrintPrologue();

            LoadBoot();

            string line;

            while (true)
            {
                try
                {
                    line = GetLine();

                    if (line == "bye")
                        break;

                    Compiler compiler = new Compiler(line, environment);
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
                    Console.Write("***Error: ");
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
            Console.WriteLine("(enter 'bye' to exit)");
            Console.WriteLine();
        }

        private static void PrintStack(Machine machine)
        {
            ICollection content = machine.StackContent;

            if (content == null || content.Count == 0)
            {
                Console.WriteLine("_empty_");
            }

            Console.Write("Stack:");

            for (int k = content.Count; --k >= 0;)
            {
                Console.Write(" ");
                Console.Write(ObjectToString(((object[])content)[k]));
            }

            Console.WriteLine();
        }

        private static string ObjectToString(object obj) 
        {
            if (obj is string) 
            {
                return string.Format("\"{0}\"", obj);
            }
            else 
            {
                return obj.ToString();
            }
        }

        private static void LoadBoot()
        {
            if (!File.Exists("Boot.ajcat"))
            {
                return;
            }

            Compiler compiler = new Compiler(File.OpenText("Boot.ajcat"), environment);

            Expression expression = compiler.CompileExpression();

            expression.Evaluate(machine);
        }
    }
}
