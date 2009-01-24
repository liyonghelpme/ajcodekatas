namespace AjPepsi
{
    using System;

    public interface IPepsiMethod : IBlock
    {
        string Name { get; }

        IClass Class { get; }
    }
}
