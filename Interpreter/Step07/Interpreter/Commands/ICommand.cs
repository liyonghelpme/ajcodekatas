
namespace Interpreter.Commands
{
    public interface ICommand
    {
        void Execute(BindingEnvironment environment);
    }
}
