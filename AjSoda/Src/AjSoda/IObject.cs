namespace AjSoda
{
    using System;

    public interface IObject
    {
        IBehavior Behavior { get; set; }

        object Send(string selector, object[] arguments);

        object GetValueAt(int position);

        void SetValueAt(int position, object value);
    }
}
