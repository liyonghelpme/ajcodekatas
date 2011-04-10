using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Scripting.Ast;

namespace DlrBinaryExpressions
{
    class Program
    {
        static void Main(string[] args)
        {
            BinaryExpression expr = Expression.Add(Expression.Constant(10),
                Expression.Constant(20));

            Console.WriteLine(expr.GetType().FullName);

            Console.WriteLine(expr.Type);
            Console.WriteLine(expr.Left.Type);
            Console.WriteLine(expr.Right.Type);

            Console.WriteLine(expr.NodeType);
            Console.WriteLine(expr.Left.NodeType);
            Console.WriteLine(expr.Right.NodeType);

            // double result = (3d + 2d) / 7d;
            // Console.WriteLine(result);

            BinaryExpression expr2 = Expression.Divide(
                Expression.Add(Expression.Constant(3d),
                                Expression.Constant(2d)),
                Expression.Constant(7d)
                );

            Func<double> binaryDelegate = Expression.Lambda<Func<double>>(expr2).Compile();

            Console.WriteLine(binaryDelegate.Invoke());
        }
    }
}
