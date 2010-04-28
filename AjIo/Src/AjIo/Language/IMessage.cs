namespace AjIo.Language
{
    using System;

    public interface IMessage
    {
        object Send(IObject context, object receiver);
    }
}
