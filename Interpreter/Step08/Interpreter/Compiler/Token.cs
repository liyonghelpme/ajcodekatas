
namespace Interpreter.Compiler
{
    public class Token
    {
        public Token(TokenType type, object value)
        {
            this.TokenType = type;
            this.Value = value;
        }

        public TokenType TokenType { get; private set; }

        public object Value { get; private set; }
    }
}
