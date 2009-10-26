namespace WebScrapping.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class WebPagesTests
    {
        [TestMethod]
        public void GetWebPage()
        {
            WebPage page = new WebPage("http://www.clarin.com");

            Assert.IsNotNull(page.Content);
        }

        [TestMethod]
        public void ParseWebPage()
        {
            WebPage page = new WebPage("http://www.clarin.com");
            ICollection<HtmlToken> tokens = page.Tokens;

            Assert.IsNotNull(tokens);
            Assert.IsTrue(tokens.Count > 0);
        }
    }
}
