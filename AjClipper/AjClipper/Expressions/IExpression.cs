namespace AjClipper.Expressions
{
    public interface IExpression
    {
        object Evaluate(AjClipper.ValueEnvironment environment);
    }
}
