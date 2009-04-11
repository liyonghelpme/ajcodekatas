namespace AjCat
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    using AjCat.Expressions;

    public class ExpressionEnvironment
    {
        private Dictionary<string, Expression> expressionsByName = new Dictionary<string, Expression>();

        public Expression GetByName(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");                    
            }

            if (expressionsByName.ContainsKey(name))
            {
                return expressionsByName[name];
            }

            throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "Unknown '{0}'", name));
        }

        public void DefineExpression(string name, Expression expression)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            expressionsByName[name] = expression;
        }
    }
}
