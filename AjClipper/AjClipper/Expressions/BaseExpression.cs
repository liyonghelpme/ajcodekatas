namespace AjClipper.Expressions
{
    public abstract class BaseExpression : AjClipper.Expressions.IExpression
    {
        public abstract object Evaluate(ValueEnvironment environment);
    }
}
