namespace AjProcessor.Providers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AjProcessor.Utilities;

    public class ActionRouteProvider : IRouteProvider
    {
        public string GetRoute(Message message)
        {
            return message.Action;
        }
    }
}
