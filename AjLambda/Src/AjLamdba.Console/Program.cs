namespace AjLamdba.Console
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    using AjLambda;
    using AjLambda.Compiler;

    public class Program
    {
        public static void Main(string[] args)
        {
            Environment environment = new Environment();

            foreach (string filename in args)
                ProcessFile(filename, environment);

            string line;

            System.Console.WriteLine("AjLambda 0.1");
            System.Console.Write("> ");
            line = System.Console.ReadLine();

            while (line != null && line.Length>0)
            {
                Parser parser = new Parser(line, environment);
                Expression expression = parser.ParseExpression();

                if (expression == null)
                {
                    System.Console.Write("> ");
                    line = System.Console.ReadLine();
                    continue;
                }

                System.Console.WriteLine(expression.ToString());

                Expression reduce;

                reduce = expression.Reduce();

                while (!reduce.Equals(expression))
                {
                    System.Console.WriteLine(reduce.ToString());
                    expression = reduce;
                    reduce = reduce.Reduce();
                }

                System.Console.Write("> ");
                line = System.Console.ReadLine();
            }
        }

        private static void ProcessFile(string filename, Environment environment)
        {
            TextReader reader = File.OpenText(filename);

            string line;

            line = reader.ReadLine();

            while (line != null)
            {
                Parser parser = new Parser(line, environment);
                parser.ParseExpression();
                line = reader.ReadLine();
            }
        }
    }
}
