using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumberTheory.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (string arg in args)
                Process(arg);

            if (args.Length > 0)
                return;

            string line;

            while ((line = System.Console.ReadLine())!=null) 
            {
                string[] words = line.Split(' ');

                foreach (string word in words)
                    Process(word);
            }
        }

        static void Process(string text)
        {
            if (text.Contains("/"))
            {
                string[] words = text.Split('/');
                int x = Int32.Parse(words[0]);
                int y = Int32.Parse(words[1]);
                ProcessLagrange(x, y);
            }

            if (text.Contains("s"))
            {
                string[] words = text.Split('s');
                int x = Int32.Parse(words[0]);
                int y = Int32.Parse(words[1]);
                ProcessSolutions(x, y);
            }

            if (text.Contains("p"))
            {
                string[] words = text.Split('p');
                int x = Int32.Parse(words[0]);
                int y = Int32.Parse(words[1]);
                ProcessPowers(x, y);
            }

            if (text.StartsWith("r"))
                ProcessResidues(GetNumber(text));
            if (text.StartsWith("n"))
                ProcessNonResidues(GetNumber(text));
            if (text.StartsWith("g"))
                ProcessGenerators(GetNumber(text));
        }

        static int GetNumber(string text)
        {
            return Int32.Parse(text.Substring(1));
        }

        static void ProcessLagrange(int x, int y)
        {
            System.Console.WriteLine(" "+ Numbers.Lagrange(x, y));
        }

        static void ProcessSolutions(int x, int y)
        {
            Modulus modulus = new Modulus(y);

            foreach (int n in modulus.Elements())
                foreach (int m in modulus.Elements())
                    if (n<=m && modulus.Multiply(n, m) == x)
                        System.Console.Write(string.Format(" {0},{1}", n, m));

            System.Console.WriteLine();
        }

        static void ProcessPowers(int x, int y)
        {
            Modulus modulus = new Modulus(y);

            Print(modulus.Powers(x));
        }

        static void ProcessGenerators(int x)
        {
            Modulus modulus = new Modulus(x);

            Print(modulus.NonZeroElements().Where(n => modulus.Powers(n).Count() == x - 1));
        }

        static void ProcessResidues(int x)
        {
            Modulus modulus = new Modulus(x);

            Print(modulus.QuadraticResidues());
        }

        static void ProcessNonResidues(int x)
        {
            Modulus modulus = new Modulus(x);

            Print(modulus.Elements().Except(modulus.QuadraticResidues()));
        }

        static void Print(IEnumerable<int> numbers)
        {
            foreach (int n in numbers)
            {
                System.Console.Write(" ");
                System.Console.Write(n);
            }

            System.Console.WriteLine();
        }
    }
}
