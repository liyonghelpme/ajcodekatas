namespace AjProcessor.Providers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface IRouteProvider
    {
        string GetRoute(Message message);
    }
}
