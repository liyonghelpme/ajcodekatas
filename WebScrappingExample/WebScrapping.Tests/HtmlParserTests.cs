namespace WebScrapping.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class HtmlParserTests
    {
        [TestMethod]
        public void ParseSimpleText()
        {
            HtmlParser parser = new HtmlParser("This is a text");
            HtmlToken token = parser.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(HtmlTokenType.Text, token.TokenType);
            Assert.AreEqual("This is a text", token.Value);

            Assert.IsNull(parser.NextToken());
        }

        [TestMethod]
        public void ParseSimpleTag()
        {
            HtmlParser parser = new HtmlParser("<html>");
            HtmlToken token = parser.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(HtmlTokenType.Tag, token.TokenType);
            Assert.AreEqual("html", token.Name);

            Assert.IsNull(parser.NextToken());
        }

        [TestMethod]
        public void ParseSimpleUpperCaseTag()
        {
            HtmlParser parser = new HtmlParser("<HTML>");
            HtmlToken token = parser.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(HtmlTokenType.Tag, token.TokenType);
            Assert.AreEqual("html", token.Name);

            Assert.IsNull(parser.NextToken());
        }

        [TestMethod]
        public void ParseSimpleMixedCaseTag()
        {
            HtmlParser parser = new HtmlParser("<Html>");
            HtmlToken token = parser.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(HtmlTokenType.Tag, token.TokenType);
            Assert.AreEqual("html", token.Name);

            Assert.IsNull(parser.NextToken());
        }

        [TestMethod]
        public void ParseSimpleClosedTag()
        {
            HtmlParser parser = new HtmlParser("<html/>");
            HtmlToken token = parser.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(HtmlTokenType.Tag, token.TokenType);
            Assert.AreEqual("html", token.Name);

            token = parser.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(HtmlTokenType.CloseTag, token.TokenType);
            Assert.AreEqual("html", token.Name);

            Assert.IsNull(parser.NextToken());
        }

        [TestMethod]
        public void ParseSimpleTagWithSpaces()
        {
            HtmlParser parser = new HtmlParser("<html  >");
            HtmlToken token = parser.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(HtmlTokenType.Tag, token.TokenType);
            Assert.AreEqual("html", token.Name);

            Assert.IsNull(parser.NextToken());
        }

        [TestMethod]
        public void ParseSimpleTagWithSimpleAttribute()
        {
            HtmlParser parser = new HtmlParser("<html attr>");
            HtmlToken token = parser.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(HtmlTokenType.Tag, token.TokenType);
            Assert.AreEqual("html", token.Name);

            token = parser.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(HtmlTokenType.Attribute, token.TokenType);
            Assert.AreEqual("attr", token.Name);
            Assert.IsNull(token.Value);

            Assert.IsNull(parser.NextToken());
        }

        [TestMethod]
        public void ParseSimpleTagWithValuedAttribute()
        {
            HtmlParser parser = new HtmlParser("<html attr=value>");
            HtmlToken token = parser.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(HtmlTokenType.Tag, token.TokenType);
            Assert.AreEqual("html", token.Name);

            token = parser.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(HtmlTokenType.Attribute, token.TokenType);
            Assert.AreEqual("attr", token.Name);
            Assert.AreEqual("value", token.Value);

            Assert.IsNull(parser.NextToken());
        }

        [TestMethod]
        public void ParseSimpleTagWithAttributeAndQuotedValue()
        {
            HtmlParser parser = new HtmlParser("<html attr='value'>");
            HtmlToken token = parser.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(HtmlTokenType.Tag, token.TokenType);
            Assert.AreEqual("html", token.Name);

            token = parser.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(HtmlTokenType.Attribute, token.TokenType);
            Assert.AreEqual("attr", token.Name);
            Assert.AreEqual("value", token.Value);

            Assert.IsNull(parser.NextToken());
        }

        [TestMethod]
        public void ParseSimpleTagWithAttributeAndTwoQuotedValues()
        {
            HtmlParser parser = new HtmlParser("<html attr='value' attr2='value2'>");
            HtmlToken token = parser.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(HtmlTokenType.Tag, token.TokenType);
            Assert.AreEqual("html", token.Name);

            token = parser.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(HtmlTokenType.Attribute, token.TokenType);
            Assert.AreEqual("attr", token.Name);
            Assert.AreEqual("value", token.Value);

            token = parser.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(HtmlTokenType.Attribute, token.TokenType);
            Assert.AreEqual("attr2", token.Name);
            Assert.AreEqual("value2", token.Value);

            Assert.IsNull(parser.NextToken());
        }

        [TestMethod]
        public void ParseSimpleTagWithAttributeAndTwoQuotedValuesAndSpaces()
        {
            HtmlParser parser = new HtmlParser("<html attr = 'value' attr2 = 'value2'>");
            HtmlToken token = parser.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(HtmlTokenType.Tag, token.TokenType);
            Assert.AreEqual("html", token.Name);

            token = parser.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(HtmlTokenType.Attribute, token.TokenType);
            Assert.AreEqual("attr", token.Name);
            Assert.AreEqual("value", token.Value);

            token = parser.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(HtmlTokenType.Attribute, token.TokenType);
            Assert.AreEqual("attr2", token.Name);
            Assert.AreEqual("value2", token.Value);

            Assert.IsNull(parser.NextToken());
        }

        [TestMethod]
        public void ParseSimpleTagSkippingDocType()
        {
            HtmlParser parser = new HtmlParser("<!doctype public><html>");
            HtmlToken token = parser.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(HtmlTokenType.Tag, token.TokenType);
            Assert.AreEqual("html", token.Name);

            Assert.IsNull(parser.NextToken());
        }

        [TestMethod]
        public void ParseSimpleCloseTag()
        {
            HtmlParser parser = new HtmlParser("</html>");
            HtmlToken token = parser.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(HtmlTokenType.CloseTag, token.TokenType);
            Assert.AreEqual("html", token.Name);

            Assert.IsNull(parser.NextToken());
        }
    }
}
