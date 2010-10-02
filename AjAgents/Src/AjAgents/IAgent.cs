namespace AjAgents
{
    using System;

    public interface IAgent<T>
    {
        void Post(Action<T> action);
    }
}
