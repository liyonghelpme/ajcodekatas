namespace AjProcessor.Providers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AjProcessor.Utilities;

    public class ApplicationRouteProvider : IRouteProvider
    {
        public string GetRoute(Message message)
        {
            return ToAddressUtilities.GetApplicationName(message.To);
        }
    }
}
