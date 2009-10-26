namespace WebScrapping
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class HtmlUtilities
    {
        public static int LocateTagPosition(string tagname, string htmltext)
        {
            return LocateTagPosition(tagname, htmltext, 0);
        }

        public static int LocateTagPosition(string tagname, string htmltext, int startposition)
        {
            string findtag = string.Format("<{0}", tagname);

            int position = htmltext.IndexOf(findtag, startposition, StringComparison.InvariantCultureIgnoreCase);

            while (position >= 0)
            {
                int endposition = position + findtag.Length;

                if (endposition >= htmltext.Length)
                    return -1;

                char ch = htmltext[endposition];

                if (char.IsWhiteSpace(ch) || ch == '/' || ch == '>')
                    return position;

                position = htmltext.IndexOf(findtag, endposition, StringComparison.InvariantCultureIgnoreCase);
            }

            return position;
        }

        public static string GetFirstTag(string htmltext)
        {
            int endposition = htmltext.IndexOf('>');
            int startposition = htmltext.IndexOf('<');

            return htmltext.Substring(startposition, endposition - startposition + 1);
        }

        public static string GetLastTag(string htmltext)
        {
            int endposition = htmltext.LastIndexOf('>');
            int startposition = htmltext.LastIndexOf('<');

            return htmltext.Substring(startposition, endposition - startposition + 1);
        }

        public static string GetTag(string tagname, string htmltext)
        {
            int position = LocateTagPosition(tagname, htmltext);

            if (position < 0)
                return null;

            int endposition = htmltext.IndexOf('>', position);

            return htmltext.Substring(position, endposition - position + 1);
        }
    }
}
