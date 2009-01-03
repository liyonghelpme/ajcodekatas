namespace AjCat.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AjCat.Expressions;

    public class Expressions
    {
        private static Dictionary<string, Expression> expressionsByName = new Dictionary<string, Expression>();

        static Expressions() 
        {
            expressionsByName["add_int"] = IntegerAddOperation.Instance;
            expressionsByName["sub_int"] = IntegerSubtractOperation.Instance;
            expressionsByName["mult_int"] = IntegerMultiplyOperation.Instance;
            expressionsByName["div_int"] = IntegerDivideOperation.Instance;

            expressionsByName["dup"] = DupExpression.Instance;
            expressionsByName["swap"] = SwapExpression.Instance;
            expressionsByName["pop"] = PopExpression.Instance;
        }

        public static Expression GetByName(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");                    
            }

            if (expressionsByName.ContainsKey(name))
            {
                return expressionsByName[name];
            }

            throw new ArgumentException(string.Format("Unknown '{0}'", name));
        }
    }
}
