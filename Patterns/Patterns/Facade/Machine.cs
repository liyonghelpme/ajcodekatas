using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Patterns.Interpreter;
using Patterns.Adapter;
using Patterns.Composite;
using System.IO;

namespace Patterns.Facade
{
    public class Machine
    {
        private IContext context;

        public Machine()
        {
            this.context = new Context();
        }

        public object GetValue(string name)
        {
            return this.context.GetValue(name);
        }

        public void SetValue(string name, object value)
        {
            this.context.SetValue(name, value);
        }

        public void Run(string text)
        {
            IParser parser = new Parser(text);
            this.Run(parser);
        }

        public void RunFile(string filename)
        {
            IParser parser = new Parser(new Lexer(File.OpenText(filename)));
            this.Run(parser);
        }

        private void Run(IParser parser)
        {
            for (ICommand command = parser.ParseCommand(); command != null; command = parser.ParseCommand())
                command.Execute(this.context);
        }
    }
}
