using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Microsoft.Scripting.Ast;

namespace DlrHelloExpressions
{
    class Program
    {
        static void Main(string[] args)
        {
            // Console.WriteLine("Hello, Method!");
            MethodInfo method = typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) });
            Expression callExpression = Expression.Call(method, Expression.Constant("Hello Method!"));
            Expression.Lambda<Action>(callExpression).Compile().Invoke();

            // string x = "Hello, Assign!";
            // Console.WriteLine(x);
            ParameterExpression x = Expression.Variable(typeof(string), "x");
            Expression blockExpression = Expression.Block(
                new ParameterExpression[] { x }, 
                Expression.Assign(x, Expression.Constant("Hello, Assign!")),
                Expression.Call(method, x));
            Expression.Lambda<Action>(blockExpression).Compile().Invoke();

            // x = "Hello";
            // if (true)
            //     x.ToLower();
            MethodInfo toLowerMethod = typeof(string).GetMethod("ToLower", new Type[] {});
            ParameterExpression y = Expression.Variable(typeof(string), "y");
            ParameterExpression z = Expression.Variable(typeof(string), "z");
            Expression blockExpression2 = Expression.Block(
                new ParameterExpression[] { x, y},
                Expression.Assign(x, Expression.Constant("Hello")),
                Expression.Condition(Expression.Constant(true),
                    Expression.Call(x, toLowerMethod),
                    Expression.Default(typeof(string)),
                    typeof(string))
                    );

            string result = Expression.Lambda<Func<string>>(blockExpression2).Compile().Invoke();
            Console.WriteLine(result);
        }
    }
}
