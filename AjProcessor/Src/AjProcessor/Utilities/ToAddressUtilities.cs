namespace AjProcessor.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public sealed class ToAddressUtilities
    {
        public const char AddressSeparator = '/';
        public const string CurrentApplicationName = ".";

        public static string GetApplicationName(string toAddress)
        {
            int position = toAddress.IndexOf(AddressSeparator);

            if (position < 0)
                return CurrentApplicationName;

            return toAddress.Substring(0, position);
        }

        public static string GetProcessorName(string toAddress)
        {
            int position = toAddress.IndexOf(AddressSeparator);

            if (position < 0)
                return toAddress;

            return toAddress.Substring(position + 1);
        }
    }
}
