namespace AjCat.Tests
{
    using System;
    using System.IO;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;

    using AjCat;
    using AjCat.Compiler;
    using AjCat.Expressions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CompilerTest
    {
        [TestMethod]
        public void CreateWithParser()
        {
            Compiler compiler = new Compiler(new Parser("name"));

            Assert.IsNotNull(compiler);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RaiseIfParserIsNull()
        {
            Compiler compiler = new Compiler((Parser)null);
        }

        [TestMethod]
        public void CreateWithText()
        {
            Compiler compiler = new Compiler("name");

            Assert.IsNotNull(compiler);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RaiseIfNameIsNull()
        {
            Compiler compiler = new Compiler((string)null);
        }

        [TestMethod]
        public void CreateWithTextReader()
        {
            Compiler compiler = new Compiler(new StringReader("name"));

            Assert.IsNotNull(compiler);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RaiseIfTextReaderIsNull()
        {
            Compiler compiler = new Compiler((TextReader)null);
        }

        [TestMethod]
        public void CompileName()
        {
            Compiler compiler = new Compiler("dup");

            Expression expression = compiler.CompileExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(DupExpression));
        }

        [TestMethod]
        public void CompileInteger()
        {
            Compiler compiler = new Compiler("10");

            Expression expression = compiler.CompileExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(IntegerExpression));
        }

        [TestMethod]
        public void CompileString()
        {
            Compiler compiler = new Compiler("\"foo\"");

            Expression expression = compiler.CompileExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(StringExpression));
            Assert.AreEqual("foo", ((StringExpression)expression).Value);
        }

        [TestMethod]
        public void CompileQuotation()
        {
            Compiler compiler = new Compiler("[1 2]");

            Expression expression = compiler.CompileExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(QuotationExpression));
        }

        [TestMethod]
        [ExpectedException(typeof(ExpectedTokenException))]
        public void RaiseIsQuotationIsNotClosed()
        {
            Compiler compiler = new Compiler("[1 2");

            compiler.CompileExpression();
        }

        [TestMethod]
        [ExpectedException(typeof(UnexpectedTokenException))]
        public void RaiseIsQuotationIsNotOpened()
        {
            Compiler compiler = new Compiler("1 2]");

            compiler.CompileExpression();
        }

        [TestMethod]
        public void CompileMultipleExpressions()
        {
            Compiler compiler = new Compiler("1 [1 2] 4 [4 5]");

            Expression expression = compiler.CompileExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(CompositeExpression));

            CompositeExpression composite = (CompositeExpression)expression;

            Assert.IsNotNull(composite.Expressions);
            Assert.AreEqual(4, composite.Expressions.Count);
            Assert.IsInstanceOfType(composite.Expressions[0], typeof(IntegerExpression));
            Assert.IsInstanceOfType(composite.Expressions[1], typeof(QuotationExpression));
            Assert.IsInstanceOfType(composite.Expressions[2], typeof(IntegerExpression));
            Assert.IsInstanceOfType(composite.Expressions[3], typeof(QuotationExpression));
        }

        [TestMethod]
        public void CompileDefine()
        {
            Compiler compiler = new Compiler("define foo { 1 2 dup }");

            Expression expression = compiler.CompileExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(DefineExpression));

            DefineExpression define = (DefineExpression)expression;

            Assert.AreEqual("foo", define.Name);
            Assert.IsNotNull(define.Expressions);
            Assert.AreEqual(3, define.Expressions.Count);
            Assert.IsInstanceOfType(define.Expressions[0], typeof(IntegerExpression));
            Assert.IsInstanceOfType(define.Expressions[1], typeof(IntegerExpression));
            Assert.IsInstanceOfType(define.Expressions[2], typeof(DupExpression));

            Assert.AreEqual("define foo { 1 2 dup }", define.ToString());
        }

        [TestMethod]
        [DeploymentItem(@"DefineTest.ajcat")]
        public void LoadDefineTest()
        {
            Compiler compiler = new Compiler(File.OpenText("DefineTest.ajcat"));

            Expression expression = compiler.CompileExpression();

            Assert.IsNotNull(expression);

            Machine machine = new Machine();

            expression.Evaluate(machine);
        }
    }
}
