namespace AjGroups
{
    using System;

    public interface IElement : IComparable<IElement>
    {
        int Order { get; }

        IElement Multiply(IElement element);

        bool IsIdentity { get; }
    }
}
