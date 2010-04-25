namespace AjIo.Language
{
    public interface IObject
    {
        object Process(Message message);

        void SetSlot(string name, object value);

        object GetSlot(string name);
    }
}
