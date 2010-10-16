using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Patterns.Composite;
using Patterns.Interpreter;
using System.IO;

namespace Patterns.Visitor
{
    public class DecompileVisitor : IVisitor
    {
        private static Dictionary<ArithmeticOperation, string> operators = new Dictionary<ArithmeticOperation, string>();

        static DecompileVisitor()
        {
            operators[ArithmeticOperation.Add] = "+";
            operators[ArithmeticOperation.Substract] = "-";
            operators[ArithmeticOperation.Multiply] = "*";
            operators[ArithmeticOperation.Divide] = "/";
        }

        private TextWriter writer;

        public DecompileVisitor(TextWriter writer)
        {
            this.writer = writer;
        }

        public void Visit(SetCommand cmd)
        {
            this.writer.Write(cmd.Name);
            this.writer.Write(" = ");
            cmd.Expression.Accept(this);
            this.writer.WriteLine(";");
        }

        public void Visit(CompositeCommand cmd)
        {
            this.writer.WriteLine("{");

            foreach (ICommand command in cmd.Commands)
                command.Accept(this);

            this.writer.WriteLine("}");
        }

        public void Visit(ArithmeticBinaryExpression expr)
        {
            this.writer.Write("(");
            expr.LeftExpression.Accept(this);            
            this.writer.Write(string.Format(" {0} ", operators[expr.Operation]));
            expr.RightExpression.Accept(this);
            this.writer.Write(")");
        }

        public void Visit(VariableExpression expr)
        {
            this.writer.Write(expr.Name);
        }

        public void Visit(ConstantExpression expr)
        {
            if (expr.Value == null)
                this.writer.Write("null");
            else if (expr.Value is string)
                this.writer.Write(string.Format("\"{0}\"", (string) expr.Value));
            else
                this.writer.Write(expr.Value.ToString());
        }

        public void Visit(IExpression expr)
        {
            throw new NotImplementedException();
        }

        public void Visit(ICommand cmd)
        {
            throw new NotImplementedException();
        }
    }
}
