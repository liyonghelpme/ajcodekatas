namespace AjSoda
{
    public interface IBehavior : IObject
    {
        IMethod Lookup(string selector);

        void AddMethod(string selector, IMethod method);
    }
}
