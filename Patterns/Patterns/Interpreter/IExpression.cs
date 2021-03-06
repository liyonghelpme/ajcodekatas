﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Patterns.Visitor;

namespace Patterns.Interpreter
{
    public interface IExpression
    {
        object Evaluate(IContext context);

        void Accept(IVisitor visitor);
    }
}
