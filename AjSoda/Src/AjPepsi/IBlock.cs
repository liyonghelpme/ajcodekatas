namespace AjPepsi
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AjSoda;

    public interface IBlock : IMethod
    {
        object Execute(PepsiMachine machine, object[] args);
    }
}
