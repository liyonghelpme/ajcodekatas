namespace AjIo.Language
{
    public interface IObject
    {
        string TypeName { get; }

        IObject Self { get; }

        object Evaluate(object expression);

        void SetSlot(string name, object value);

        object GetSlot(string name);

        void UpdateSlot(string name, object value);
    }
}
