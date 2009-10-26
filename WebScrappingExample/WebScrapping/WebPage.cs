namespace WebScrapping
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;

    public class WebPage
    {
        private string address;
        private string content;
        private List<HtmlToken> tokens;

        public WebPage(string address)
        {
            this.address = address;
        }

        public string Content
        {
            get
            {
                if (this.content == null)
                    this.content = this.GetContent();

                return this.content;
            }
        }

        public ICollection<HtmlToken> Tokens
        {
            get
            {
                if (this.tokens == null)
                    this.tokens = this.GetTokens();

                return this.tokens;
            }
        }

        private string GetContent()
        {
            WebClient webclient = new WebClient();
            return webclient.DownloadString(this.address);
        }

        private List<HtmlToken> GetTokens()
        {
            HtmlParser parser = new HtmlParser(this.Content);
            List<HtmlToken> tokens = new List<HtmlToken>();

            try
            {
                for (HtmlToken token = parser.NextToken(); token != null; token = parser.NextToken())
                    tokens.Add(token);
            }
            catch (Exception)
            {
            }

            return tokens;
        }
    }
}
