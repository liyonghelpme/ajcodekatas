using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Patterns.Composite;
using Patterns.Interpreter;

namespace Patterns.Visitor
{
    public interface IVisitor
    {
        void Visit(SetCommand cmd);
        void Visit(CompositeCommand cmd);

        void Visit(ArithmeticBinaryExpression expr);
        void Visit(VariableExpression expr);
        void Visit(ConstantExpression expr);

        void Visit(IExpression expr);
        void Visit(ICommand cmd);
    }
}
