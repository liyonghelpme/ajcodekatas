namespace DynamicExpressionsExample
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Linq.Dynamic;
    using System.Text;

    class Calculator
    {
        public Delegate CompileFormula(string formula)
        {
            ParameterExpression x = Expression.Parameter(typeof(double), "x");
            LambdaExpression e = DynamicExpression.ParseLambda(new ParameterExpression[] { x }, null, formula);
            return e.Compile();
        }

        public double[] Calculate(Delegate formula, double from, double to, double step)
        {
            List<double> values = new List<double>();

            for (double x = from; x <= to; x += step)
                values.Add((double) formula.DynamicInvoke(x));

            return values.ToArray();
        }
    }
}
