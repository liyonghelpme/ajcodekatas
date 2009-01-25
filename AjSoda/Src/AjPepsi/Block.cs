namespace AjPepsi
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AjPepsi.Compiler;
    using AjSoda;

    public class Block : IBlock
    {
        private byte[] byteCodes;
        private short nextByteCode;
        private List<object> constants = new List<object>();
        private List<string> argumentNames = new List<string>();
        private List<string> localNames = new List<string>();
        private List<string> globalNames = new List<string>();

        public int Arity
        {
            get
            {
                return this.argumentNames.Count;
            }
        }

        public byte[] ByteCodes
        {
            get
            {
                return this.byteCodes;
            }
        }

        public int NoLocals
        {
            get
            {
                if (this.localNames == null)
                {
                    return 0;
                }

                return this.localNames.Count;
            }
        }

        public int NoConstants
        {
            get
            {
                if (this.constants == null)
                {
                    return 0;
                }

                return this.constants.Count;
            }
        }

        public static byte MessageArity(string messageName)
        {
            if (!Char.IsLetter(messageName[0]))
            {
                return 2;
            }

            int p = messageName.IndexOf(':');

            if (p < 0)
            {
                return 0;
            }

            byte n = 0;

            foreach (char ch in messageName)
            {
                if (ch == ':')
                {
                    n++;
                }
            }

            return n;
        }

        public void CompileArgument(string argumentName)
        {
            if (this.argumentNames.Contains(argumentName))
            {
                throw new CompilerException("Repeated Argument: " + argumentName);
            }

            this.argumentNames.Add(argumentName);
        }

        public void CompileLocal(string localName)
        {
            if (this.localNames.Contains(localName))
            {
                throw new CompilerException("Repeated Local: " + localName);
            }

            this.localNames.Add(localName);
        }

        public byte CompileConstant(object value)
        {
            int p = this.constants.IndexOf(value);

            if (p >= 0)
            {
                return (byte)p;
            }

            this.constants.Add(value);

            return (byte)(this.constants.Count - 1);
        }

        public byte CompileGlobal(string globalName)
        {
            int p = this.globalNames.IndexOf(globalName);

            if (p >= 0)
            {
                return (byte)p;
            }

            this.globalNames.Add(globalName);

            return (byte)(this.globalNames.Count - 1);
        }

        public void CompileGetConstant(object value)
        {
            this.CompileByteCode(ByteCode.GetConstant, this.CompileConstant(value));
        }

        public void CompileReturnPop()
        {
            this.CompileByteCode(ByteCode.ReturnPop);
        }

        public void CompileGetBlock(object block)
        {
            this.CompileByteCode(ByteCode.GetBlock, this.CompileConstant(block));
        }

        public void CompileByteCode(ByteCode byteCode)
        {
            this.CompileByte((byte)byteCode);
        }

        public void CompileByteCode(ByteCode byteCode, byte argument)
        {
            this.CompileByteCode(byteCode);
            this.CompileByte(argument);
        }

        public void CompileByteCode(ByteCode byteCode, byte firstArgument, byte secondArgument)
        {
            this.CompileByteCode(byteCode);
            this.CompileByte(firstArgument);
            this.CompileByte(secondArgument);
        }

        public void CompileInvokeDotNet(string messageName)
        {
            messageName = messageName.Substring(1);

            int p = messageName.IndexOf(':');

            string mthname;

            if (p >= 0)
            {
                mthname = messageName.Substring(0, p);
            }
            else
            {
                mthname = messageName;
            }

            if (mthname == "new")
            {
                this.CompileByteCode(ByteCode.NewDotNetObject, MessageArity(messageName));
            }
            else
            {
                this.CompileByteCode(ByteCode.InvokeDotNetMethod, this.CompileConstant(mthname), MessageArity(messageName));
            }
        }

        public void CompileSend(string messageName)
        {
            if (messageName[0] == Tokenizer.SpecialDotNetInvokeMark)
            {
                this.CompileInvokeDotNet(messageName);
                return;
            }

            if (messageName == "instSize")
            {
                this.CompileByteCode(ByteCode.InstSize);
            }
            else if (messageName == "instAt:")
            {
                this.CompileByteCode(ByteCode.InstAt);
            }
            else if (messageName == "instAt:put:")
            {
                this.CompileByteCode(ByteCode.InstAtPut);
            }
            else if (messageName == "class")
            {
                this.CompileByteCode(ByteCode.GetClass);
            }
            else
            {
                this.CompileByteCode(ByteCode.Send, this.CompileConstant(messageName), MessageArity(messageName));
            }
        }

        public virtual object Execute(object receiver, params object[] arguments)
        {
            return (new ExecutionBlock((IObject)receiver, this, arguments)).Execute();
        }

        public virtual void CompileGet(string name)
        {
            if (this.TryCompileGet(name))
            {
                return;
            }

            this.CompileByteCode(ByteCode.GetGlobalVariable, this.CompileGlobal(name));
        }

        public virtual void CompileGetDotNetType(string name)
        {
            this.CompileByteCode(ByteCode.GetDotNetType, this.CompileGlobal(name));
        }

        public virtual void CompileSet(string name)
        {
            if (this.TryCompileSet(name))
            {
                return;
            }

            this.CompileByteCode(ByteCode.SetGlobalVariable, this.CompileGlobal(name));
        }

        public object GetConstant(int nc)
        {
            return this.constants[nc];
        }

        public string GetGlobalName(int ng)
        {
            return this.globalNames[ng];
        }

        protected bool TryCompileGet(string name)
        {
            if (name == "self")
            {
                this.CompileByteCode(ByteCode.GetSelf);
                return true;
            }

            int p;

            if (this.localNames != null)
            {
                p = this.localNames.IndexOf(name);

                if (p >= 0)
                {
                    this.CompileByteCode(ByteCode.GetLocal, (byte)p);

                    return true;
                }
            }

            if (this.argumentNames != null)
            {
                p = this.argumentNames.IndexOf(name);

                if (p >= 0)
                {
                    this.CompileByteCode(ByteCode.GetArgument, (byte)p);
                    return true;
                }
            }

            return false;
        }

        protected bool TryCompileSet(string name)
        {
            int p;

            if (this.localNames != null)
            {
                p = this.localNames.IndexOf(name);

                if (p >= 0)
                {
                    this.CompileByteCode(ByteCode.SetLocal, (byte)p);
                    return true;
                }
            }

            if (this.argumentNames != null)
            {
                p = this.argumentNames.IndexOf(name);

                if (p >= 0)
                {
                    this.CompileByteCode(ByteCode.SetArgument, (byte)p);
                    return true;
                }
            }

            return false;
        }

        private void CompileByte(byte b)
        {
            if (this.byteCodes == null)
            {
                this.byteCodes = new byte[] { b };
                this.nextByteCode = 1;
                return;
            }

            if (this.nextByteCode >= this.byteCodes.Length)
            {
                byte[] aux = new byte[this.byteCodes.Length + 10];
                Array.Copy(this.byteCodes, aux, this.byteCodes.Length);
                this.byteCodes = aux;
            }

            this.byteCodes[this.nextByteCode++] = b;
        }
    }
}
