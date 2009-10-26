using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WebScrapping.Demo
{
    class Program
    {
        static Dictionary<string, string> letterpages = new Dictionary<string, string>();
        static Dictionary<string, string> drugpages = new Dictionary<string, string>();
        static Dictionary<string, string> pages = new Dictionary<string, string>();

        static void Main(string[] args)
        {
            string currentlink = null;

            WebPage page = new WebPage("http://www.nlm.nih.gov/medlineplus/druginformation.html");

            foreach (HtmlToken token in page.Tokens)
            {
                switch (token.TokenType)
                {
                    case HtmlTokenType.Text:
                        if (currentlink != null)
                            AddLetterLink(currentlink, token.Value);

                        currentlink = null;
                        break;
                    case HtmlTokenType.Tag:
                        Console.WriteLine(string.Format("<{0}>", token.Name));
                        currentlink = null;

                        break;
                    case HtmlTokenType.Attribute:
                        if (token.Value == null)
                            Console.WriteLine(string.Format("{0}=", token.Name));
                        else
                            Console.WriteLine(string.Format("{0}={1}", token.Name, token.Value));

                        if (token.Name == "href" && token.Value != null && token.Value.Contains("/drug_"))
                            currentlink = token.Value;
                        else
                            currentlink = null;

                        break;
                }
            }

            Console.WriteLine();

            foreach (string key in letterpages.Keys.OrderBy(k => k.ToUpper()))
            {
                Console.WriteLine(string.Format("{0} {1}", letterpages[key], key));

                HarvestPage(letterpages[key]);
            }

            foreach (string key in drugpages.Keys.OrderBy(k => k.ToUpper()))
            {
                Console.WriteLine(string.Format("{0} {1}", drugpages[key], key));
                GetPage(key, drugpages[key]);
            }

            CreateIndexPage();
        }

        private static void AddLetterLink(string address, string text)
        {
            letterpages[text] = address;
        }

        private static void HarvestPage(string address)
        {
            int position = address.LastIndexOf('/');
            string baseaddress = address.Substring(0, position);

            WebPage page = new WebPage(address);
            string currentlink = null;

            foreach (HtmlToken token in page.Tokens)
            {
                switch (token.TokenType)
                {
                    case HtmlTokenType.Text:
                        if (currentlink != null)
                            AddDrugLink(currentlink, token.Value);

                        currentlink = null;
                        break;
                    case HtmlTokenType.Tag:
                        currentlink = null;

                        break;
                    case HtmlTokenType.Attribute:
                        if (token.Name == "href" && token.Value != null && token.Value.StartsWith("meds/"))
                            currentlink = baseaddress + "/" + token.Value;
                        else
                            currentlink = null;

                        break;
                }
            }
        }

        private static void AddDrugLink(string address, string text)
        {
            drugpages[text] = address;
        }

        private static void GetPage(string title, string address)
        {
            WebPage page = new WebPage(address);
            string content = page.Content;

            string pagename = string.Format("drug{0}.html", pages.Count);

            int position = content.IndexOf("<H3>");

            if (position < 0)
                return;

            content = content.Substring(position);

            position = content.IndexOf("<table", StringComparison.InvariantCultureIgnoreCase);
            int position2 = content.IndexOf("</table>", StringComparison.InvariantCultureIgnoreCase);

            if (position > 0 && position2 > 0)
                content = content.Substring(0, position) + content.Substring(position2 + 9);

            position = content.LastIndexOf("</h4>", StringComparison.InvariantCultureIgnoreCase);

            if (position > 0)
                position = content.IndexOf("<br", position, StringComparison.InvariantCultureIgnoreCase);

            if (position > 0)
                content = content.Substring(0, position);

            //File.WriteAllText(pagename, content);

            pages[title] = pagename;
        }

        private static void CreateIndexPage()
        {
            StreamWriter writer = new StreamWriter("index.html");

            writer.WriteLine("<h1>Drug Information</h1>");

            writer.WriteLine("<ul>");

            foreach (string key in pages.Keys)
                writer.WriteLine(string.Format("<li><a href='{1}'>{0}</1></li>", key, pages[key]));

            writer.WriteLine("</ul>");

            writer.Close();

        }
    }
}
