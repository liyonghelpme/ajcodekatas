namespace AjSoda
{
    using System;

    public interface IObject
    {
        IBehavior Behavior { get; set; }

        object Send(string selector, object[] arguments);
    }
}
