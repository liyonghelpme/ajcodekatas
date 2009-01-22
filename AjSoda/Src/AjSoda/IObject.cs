namespace AjSoda
{
    using System;

    public interface IObject
    {
        IObject Behavior { get; set; }

        int Size { get; }

        object Send(string selector, params object[] arguments);

        object GetValueAt(int position);

        void SetValueAt(int position, object value);
    }
}
