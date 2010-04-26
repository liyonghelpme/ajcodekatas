namespace AjIo.Language
{
    public interface IObject
    {
        object Evaluate(object expression);

        void SetSlot(string name, object value);

        object GetSlot(string name);

        string TypeName { get; }
    }
}
