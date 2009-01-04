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
            expressionsByName["inc"] = IntegerIncrementOperation.Instance;
            expressionsByName["dec"] = IntegerDecrementOperation.Instance;
            expressionsByName["mod"] = IntegerModuleOperation.Instance;

            expressionsByName["dup"] = DupExpression.Instance;
            expressionsByName["swap"] = SwapExpression.Instance;
            expressionsByName["pop"] = PopExpression.Instance;
            expressionsByName["#clr"] = ClearExpression.Instance;

            expressionsByName["nil"] = NilExpression.Instance;
            expressionsByName["cons"] = ConsExpression.Instance;
            expressionsByName["uncons"] = UnconsExpression.Instance;
            expressionsByName["list"] = ListExpression.Instance;
            expressionsByName["empty"] = EmptyExpression.Instance;

            expressionsByName["if"] = IfExpression.Instance;
            expressionsByName["or"] = OrExpression.Instance;
            expressionsByName["and"] = AndExpression.Instance;
            expressionsByName["true"] = new TrueExpression();
            expressionsByName["false"] = new FalseExpression();
            expressionsByName["not"] = NotExpression.Instance;
            expressionsByName["eq"] = EqualsExpression.Instance;

            expressionsByName["apply"] = ApplyExpression.Instance;
            expressionsByName["papply"] = PartialApplyExpression.Instance;
            expressionsByName["compose"] = ComposeExpression.Instance;
            expressionsByName["quote"] = QuoteExpression.Instance;
            expressionsByName["dip"] = DipExpression.Instance;
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

        public static void DefineExpression(string name, Expression expression)
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
