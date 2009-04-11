namespace AjCat.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AjCat.Expressions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class EnvironmentTest
    {

        private ExpressionEnvironment environment;

        [TestInitialize()]
        public void MyTestInitialize() 
        {
            this.environment = new TopExpressionEnvironment();
        }
        
         [TestMethod]
         public void RetrievesIntegerExpressionsByName()
         {
             Dictionary<string, Type> types = new Dictionary<string, Type>();

             types["add_int"] = typeof(IntegerAddOperation);
             types["sub_int"] = typeof(IntegerSubtractOperation);
             types["mult_int"] = typeof(IntegerMultiplyOperation);
             types["div_int"] = typeof(IntegerDivideOperation);
             types["inc"] = typeof(IntegerIncrementOperation);
             types["dec"] = typeof(IntegerDecrementOperation);
             types["mod_int"] = typeof(IntegerModuleOperation);
             types["lt_int"] = typeof(IntegerLessThanExpression);
             types["lteq_int"] = typeof(IntegerLessEqualThanExpression);
             types["gt_int"] = typeof(IntegerGreaterThanExpression);
             types["gteq_int"] = typeof(IntegerGreaterEqualThanExpression);

             this.GetTypes(types);
         }

         [TestMethod]
         public void RetrievesDoubleExpressionsByName()
         {
             Dictionary<string, Type> types = new Dictionary<string, Type>();

             types["add_dbl"] = typeof(DoubleAddOperation);
             types["sub_dbl"] = typeof(DoubleSubtractOperation);
             types["mult_dbl"] = typeof(DoubleMultiplyOperation);
             types["div_dbl"] = typeof(DoubleDivideOperation);

             types["abs_dbl"] = typeof(DoubleAbsoluteOperation);
             types["acos_dbl"] = typeof(DoubleArcCosineOperation);
             types["cos_dbl"] = typeof(DoubleCosineOperation);
             types["asin_dbl"] = typeof(DoubleArcSineOperation);
             types["sin_dbl"] = typeof(DoubleSineOperation);
             types["tan_dbl"] = typeof(DoubleTangentOperation);
             types["atan_dbl"] = typeof(DoubleArcTangentOperation);
             types["atan2_dbl"] = typeof(DoubleArcTangent2Operation);
             types["sinh_dbl"] = typeof(DoubleSineHyperbolicOperation);
             types["cosh_dbl"] = typeof(DoubleCosineHyperbolicOperation);
             types["tanh_dbl"] = typeof(DoubleTangentHyperbolicOperation);

             this.GetTypes(types);
         }

         [TestMethod]
         public void RetrievesAsExpressionsByName()
         {
             Dictionary<string, Type> types = new Dictionary<string, Type>();

             types["as_int"] = typeof(AsIntegerExpression);
             types["as_dbl"] = typeof(AsDoubleExpression);
             types["as_string"] = typeof(AsStringExpression);
             types["as_bool"] = typeof(AsBoolExpression);
             types["as_char"] = typeof(AsCharExpression);
             types["as_var"] = typeof(AsVarExpression);
             types["as_list"] = typeof(AsListExpression);

             this.GetTypes(types);
         }

         [TestMethod]
         public void RetrievesStackExpressionsByName()
         {
             Dictionary<string, Type> types = new Dictionary<string, Type>();

             types["#clr"] = typeof(ClearExpression);
             types["pop"] = typeof(PopExpression);
             types["dup"] = typeof(DupExpression);
             types["swap"] = typeof(SwapExpression);
             types["id"] = typeof(IdExpression);

             this.GetTypes(types);
         }

         [TestMethod]
         public void RetrievesListExpressionsByName()
         {
             Dictionary<string, Type> types = new Dictionary<string, Type>();

             types["nil"] = typeof(NilExpression);
             types["list"] = typeof(ListExpression);
             types["cons"] = typeof(ConsExpression);
             types["uncons"] = typeof(UnconsExpression);
             types["empty"] = typeof(EmptyExpression);

             this.GetTypes(types);
         }

         [TestMethod]
         public void RetrievesControlExpressionsByName()
         {
             Dictionary<string, Type> types = new Dictionary<string, Type>();

             types["if"] = typeof(IfExpression);
             types["while"] = typeof(WhileExpression);

             this.GetTypes(types);
         }

         [TestMethod]
         public void RetrievesBooleanExpressionsByName()
         {
             Dictionary<string, Type> types = new Dictionary<string, Type>();

             types["not"] = typeof(NotExpression);
             types["and"] = typeof(AndExpression);
             types["or"] = typeof(OrExpression);
             types["false"] = typeof(FalseExpression);
             types["true"] = typeof(TrueExpression);
             types["eq"] = typeof(EqualsExpression);

             this.GetTypes(types);
         }

         [TestMethod]
         public void RetrievesFunctionalExpressionsByName()
         {
             Dictionary<string, Type> types = new Dictionary<string, Type>();

             types["A"] = typeof(AExpression);
             types["#load"] = typeof(LoadExpression);
             types["eval"] = typeof(EvalExpression);
             types["papply"] = typeof(PartialApplyExpression);
             types["apply"] = typeof(ApplyExpression);
             types["compose"] = typeof(ComposeExpression);
             types["quote"] = typeof(QuoteExpression);
             types["dip"] = typeof(DipExpression);

             this.GetTypes(types);
         }

         [TestMethod]
         public void DefineNewExpression()
         {
             Expression expression = new StringExpression("bar");
             this.environment.DefineExpression("foo", expression);

             Assert.IsNotNull(this.environment.GetByName("foo"));
             Assert.AreEqual(expression, this.environment.GetByName("foo"));
         }

         [TestMethod]
         [ExpectedException(typeof(ArgumentNullException))]
         public void RaiseIfNameIsNullInDefine()
         {
             this.environment.DefineExpression(null, new StringExpression("foo"));
         }

         [TestMethod]
         [ExpectedException(typeof(ArgumentNullException))]
         public void RaiseIfExpressionIsNullInDefine()
         {
             this.environment.DefineExpression("foo", null);
         }

         [TestMethod]
         public void RedefineExpression()
         {
             Expression expression = new StringExpression("bar");
             this.environment.DefineExpression("foo", expression);
             Assert.IsNotNull(this.environment.GetByName("foo"));

             Expression newexpression = new StringExpression("foo");

             this.environment.DefineExpression("foo", newexpression);

             Assert.AreEqual(newexpression, this.environment.GetByName("foo"));
         }

         private Expression GetByName(string name)
         {
             return this.environment.GetByName(name);
         }

         private void GetTypes(Dictionary<string, Type> types)
         {
             foreach (string name in types.Keys)
             {
                 Expression expression = this.GetByName(name);
                 Assert.IsInstanceOfType(expression, types[name]);
                 Assert.AreEqual(name, expression.ToString());
             }
         }
    }
}
