namespace AjCat
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    using AjCat.Expressions;

    public class TopExpressionEnvironment : ExpressionEnvironment
    {
        public TopExpressionEnvironment() 
        {
            this.DefineExpression("add_int", IntegerAddOperation.Instance);
            this.DefineExpression("sub_int", IntegerSubtractOperation.Instance);
            this.DefineExpression("mult_int", IntegerMultiplyOperation.Instance);
            this.DefineExpression("div_int", IntegerDivideOperation.Instance);
            this.DefineExpression("inc", IntegerIncrementOperation.Instance);
            this.DefineExpression("dec", IntegerDecrementOperation.Instance);
            this.DefineExpression("mod_int", IntegerModuleOperation.Instance);

            this.DefineExpression("add_dbl", DoubleAddOperation.Instance);
            this.DefineExpression("sub_dbl", DoubleSubtractOperation.Instance);
            this.DefineExpression("mult_dbl", DoubleMultiplyOperation.Instance);
            this.DefineExpression("div_dbl", DoubleDivideOperation.Instance);
            this.DefineExpression("abs_dbl", DoubleAbsoluteOperation.Instance);
            this.DefineExpression("cos_dbl", DoubleCosineOperation.Instance);
            this.DefineExpression("acos_dbl", DoubleArcCosineOperation.Instance);
            this.DefineExpression("sin_dbl", DoubleSineOperation.Instance);
            this.DefineExpression("asin_dbl", DoubleArcSineOperation.Instance);
            this.DefineExpression("tan_dbl", DoubleTangentOperation.Instance);
            this.DefineExpression("atan_dbl", DoubleArcTangentOperation.Instance);
            this.DefineExpression("atan2_dbl", DoubleArcTangent2Operation.Instance);
            this.DefineExpression("sinh_dbl", DoubleSineHyperbolicOperation.Instance);
            this.DefineExpression("cosh_dbl", DoubleCosineHyperbolicOperation.Instance);
            this.DefineExpression("tanh_dbl", DoubleTangentHyperbolicOperation.Instance);

            this.DefineExpression("id", IdExpression.Instance);
            this.DefineExpression("dup", DupExpression.Instance);
            this.DefineExpression("swap", SwapExpression.Instance);
            this.DefineExpression("pop", PopExpression.Instance);
            this.DefineExpression("#clr", ClearExpression.Instance);

            this.DefineExpression("nil", NilExpression.Instance);
            this.DefineExpression("cons", ConsExpression.Instance);
            this.DefineExpression("uncons", UnconsExpression.Instance);
            this.DefineExpression("list", ListExpression.Instance);
            this.DefineExpression("empty", EmptyExpression.Instance);

            this.DefineExpression("if", IfExpression.Instance);
            this.DefineExpression("while", WhileExpression.Instance);

            this.DefineExpression("or", OrExpression.Instance);
            this.DefineExpression("and", AndExpression.Instance);
            this.DefineExpression("true", new TrueExpression());
            this.DefineExpression("false", new FalseExpression());
            this.DefineExpression("not", NotExpression.Instance);
            this.DefineExpression("eq", EqualsExpression.Instance);
            this.DefineExpression("lt_int", IntegerLessThanExpression.Instance);
            this.DefineExpression("lteq_int", IntegerLessEqualThanExpression.Instance);
            this.DefineExpression("gt_int", IntegerGreaterThanExpression.Instance);
            this.DefineExpression("gteq_int", IntegerGreaterEqualThanExpression.Instance);

            this.DefineExpression("#load", LoadExpression.Instance);
            this.DefineExpression("A", AExpression.Instance);
            this.DefineExpression("apply", ApplyExpression.Instance);
            this.DefineExpression("eval", EvalExpression.Instance);
            this.DefineExpression("papply", PartialApplyExpression.Instance);
            this.DefineExpression("compose", ComposeExpression.Instance);
            this.DefineExpression("quote", QuoteExpression.Instance);
            this.DefineExpression("dip", DipExpression.Instance);

            this.DefineExpression("as_bool", AsBoolExpression.Instance);
            this.DefineExpression("as_int", AsIntegerExpression.Instance);
            this.DefineExpression("as_string", AsStringExpression.Instance);
            this.DefineExpression("as_char", AsCharExpression.Instance);
            this.DefineExpression("as_var", AsVarExpression.Instance);
            this.DefineExpression("as_list", AsListExpression.Instance);
            this.DefineExpression("as_dbl", AsDoubleExpression.Instance);
        }
    }
}
