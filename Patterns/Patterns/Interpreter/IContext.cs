using System;
namespace Patterns.Interpreter
{
    public interface IContext
    {
        object GetValue(string name);
        void SetValue(string name, object value);
    }
}
