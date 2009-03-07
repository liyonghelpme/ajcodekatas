namespace AjProcessor.Providers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AjProcessor.Utilities;

    public class ProcessorRouteProvider : IRouteProvider
    {
        public string GetRoute(Message message)
        {
            return ToAddressUtilities.GetProcessorName(message.To);
        }
    }
}
