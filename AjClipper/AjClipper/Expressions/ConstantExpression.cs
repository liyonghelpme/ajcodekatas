namespace AjClipper.Expressions
{
    public class ConstantExpression : BaseExpression
    {
        private object value;

        public ConstantExpression(object value)
        {
            this.value = value;
        }

        public override object Evaluate(ValueEnvironment environment)
        {
            return this.value;
        }
    }
}
