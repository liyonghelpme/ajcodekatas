namespace AjSoda
{
    public interface IMethod
    {
        object Execute(object receiver, params object[] arguments);
    }
}
