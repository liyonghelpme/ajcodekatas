namespace AjClipper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjClipper.Expressions;

    public static class EvaluateUtilities
    {
        public static string EvaluateAsName(IExpression expression, ValueEnvironment environment)
        {
            if (expression == null)
                return null;

            if (expression is NameExpression)
                return ((NameExpression)expression).Name;

            if (expression is DotExpression)
            {
                DotExpression dot = (DotExpression)expression;

                return EvaluateAsName(dot.Expression, environment) + "." + dot.Name;
            }

            object result = expression.Evaluate(environment);

            if (result == null)
                return null;

            return result.ToString();
        }
    }
}
