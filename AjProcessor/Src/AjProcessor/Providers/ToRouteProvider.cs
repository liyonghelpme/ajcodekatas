namespace AjProcessor.Providers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ToRouteProvider : IRouteProvider
    {
        public string GetRoute(Message message)
        {
            return message.To;
        }
    }
}
