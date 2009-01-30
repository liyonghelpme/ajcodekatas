namespace AjGroups
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface IGroup
    {
        List<Element> Elements { get; }

        int Order { get; }
    }
}
