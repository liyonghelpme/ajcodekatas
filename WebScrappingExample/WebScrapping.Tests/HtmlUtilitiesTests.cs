namespace WebScrapping.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class HtmlUtilitiesTests
    {
        [TestMethod]
        public void LocateSimpleTagPosition()
        {
            int position = HtmlUtilities.LocateTagPosition("body", "<html><body></body></html>");

            Assert.AreEqual(6, position);
        }

        [TestMethod]
        public void LocateSimpleTagPositionSkippingTag()
        {
            int position = HtmlUtilities.LocateTagPosition("body", "<html><body2/><body></body></html>");

            Assert.AreEqual(14, position);
        }

        [TestMethod]
        public void LocateClosedTag()
        {
            int position = HtmlUtilities.LocateTagPosition("body", "<html><body2/><body/></html>");

            Assert.AreEqual(14, position);
        }

        [TestMethod]
        public void LocateSecondTag()
        {
            int position = HtmlUtilities.LocateTagPosition("p", "<h1><p></p><p></p></h1>");
            position = HtmlUtilities.LocateTagPosition("p", "<h1><p></p><p></p></h1>", position + 1);

            Assert.AreEqual(11, position);
        }

        [TestMethod]
        public void GetLastTag()
        {
            string tag = HtmlUtilities.GetLastTag("<h1><p></p><p></p></h1>");

            Assert.IsNotNull(tag);
            Assert.AreEqual("</h1>", tag);
        }

        [TestMethod]
        public void GetFirstTag()
        {
            string tag = HtmlUtilities.GetFirstTag("<h1><p></p><p></p></h1>");

            Assert.IsNotNull(tag);
            Assert.AreEqual("<h1>", tag);
        }

        [TestMethod]
        public void GetSimpleTag()
        {
            string tag = HtmlUtilities.GetTag("p", "<h1><p></p><p></p></h1>");

            Assert.IsNotNull(tag);
            Assert.AreEqual("<p>", tag);
        }

        [TestMethod]
        public void GetTagWithAttribute()
        {
            string tag = HtmlUtilities.GetTag("p", "<h1><p align='right'></p><p></p></h1>");

            Assert.IsNotNull(tag);
            Assert.AreEqual("<p align='right'>", tag);
        }
    }
}
