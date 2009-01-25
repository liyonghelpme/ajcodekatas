namespace AjPepsi
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;

    using AjPepsi.Compiler;
    using AjSoda;

    public class Evaluator
    {
        private Tokenizer tokenizer;
        private PepsiMachine machine;

        public Evaluator(PepsiMachine machine, Tokenizer tokenizer)
        {
            if (machine == null) 
            {
                throw new ArgumentNullException("machine");
            }

            this.machine = machine;
            this.tokenizer = tokenizer;
        }

        public Evaluator(PepsiMachine machine, string text)
            : this(machine, new Tokenizer(text))
        {
        }

        public Evaluator(PepsiMachine machine, TextReader reader)
            : this(machine, new Tokenizer(reader))
        {
        }

        public void Evaluate()
        {
            while (this.EvaluateCommand())
            {
            }
        }

        private bool EvaluateCommand()
        {
            Token token = this.tokenizer.NextToken();

            if (token == null)
            {
                return false;
            }

            if (token.Value == "[")
            {
                tokenizer.PushToken(token);
                Compiler.Compiler compiler = new Compiler.Compiler(this.tokenizer);
                Block block = compiler.CompileEnclosedBlock();
                block.Execute(this.machine);
                return true;
            }

            if (token.Type == TokenType.Name)
            {
                this.EvaluateName(token.Value);
                return true;
            }

            return false;
        }

        private void EvaluateName(string name)
        {
            Token token = tokenizer.NextToken();

            if (token == null)
            {
                throw new Compiler.EndOfInputException();
            }

            if (token.Value == ":=")
            {
                this.EvaluateAssignment(name);
                return;
            }

            if (token.Value == ":")
            {
                this.EvaluateSubclass(name);
                return;
            }

            if (token.Type == TokenType.Name)
            {
                tokenizer.PushToken(token);
                this.EvaluateDefine(name);
                return;
            }

            throw new Compiler.CompilerException(string.Format(CultureInfo.InvariantCulture, "Unexpected '{0}'", name));
        }

        private void EvaluateSubclass(string name)
        {
            Token token = tokenizer.NextToken();

            if (token == null)
            {
                throw new Compiler.EndOfInputException();
            }

            if (token.Type != TokenType.Name)
            {
                throw new Compiler.CompilerException(string.Format(CultureInfo.InvariantCulture, "Unexpected '{0}'", token.Value));
            }

            this.machine.CreatePrototype(name, token.Value);
        }

        private void EvaluateAssignment(string name)
        {
            Compiler.Compiler compiler = new Compiler.Compiler(this.tokenizer);

            Block block = compiler.CompileEnclosedBlock();

            this.machine.SetGlobalObject(name, block.Execute(this.machine));
        }

        private void EvaluateDefine(string name)
        {
            IObject obj = (IObject)this.machine.GetGlobalObject(name);

            Compiler.Compiler compiler = new Compiler.Compiler(this.tokenizer);

            compiler.CompileInstanceMethod(((IClass)obj.Behavior));
        }
    }
}
