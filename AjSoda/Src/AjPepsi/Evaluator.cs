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
                this.tokenizer.PushToken(token);
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
            Token token = this.tokenizer.NextToken();

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
                this.EvaluateSubprototype(name);
                return;
            }

            if (token.Type == TokenType.Name)
            {
                this.tokenizer.PushToken(token);
                this.EvaluateDefine(name);
                return;
            }

            throw new UnexpectedTokenException(token);
        }

        private void EvaluateSubprototype(string name)
        {
            Token token = this.GetName();
            string superName = token.Value;

            this.GetToken("(");

            List<string> variableNames = new List<string>();

            token = this.tokenizer.NextToken();

            while (token != null && token.Type == TokenType.Name)
            {
                variableNames.Add(token.Value);
                token = this.tokenizer.NextToken();
            }

            this.tokenizer.PushToken(token);

            this.GetToken(")");

            this.machine.CreatePrototype(name, superName, variableNames);
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

        private Token GetName()
        {
            Token token = this.tokenizer.NextToken();

            if (token == null)
            {
                throw new EndOfInputException();
            }

            if (token.Type != TokenType.Name)
            {
                throw new UnexpectedTokenException(token);
            }

            return token;
        }


        private Token GetToken(string value)
        {
            Token token = this.tokenizer.NextToken();

            if (token == null)
            {
                throw new EndOfInputException();
            }

            if (token.Value != value)
            {
                throw new UnexpectedTokenException(token);
            }

            return token;
        }
    }
}
