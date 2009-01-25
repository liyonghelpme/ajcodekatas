namespace AjPepsi.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AjPepsi;
    using AjPepsi.Compiler;
    using AjSoda;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CompilerTests
    {
        public static IClass CompileClass(string clsname, string[] varnames, string[] methods)
        {
            PepsiMachine machine = new PepsiMachine();
            IClass cls = machine.CreateClass();

            if (varnames != null)
            {
                foreach (string varname in varnames)
                {
                    cls.AddVariable(varname);
                }
            }

            if (methods != null)
            {
                foreach (string method in methods)
                {
                    Compiler compiler = new Compiler(method);
                    compiler.CompileInstanceMethod(cls);
                }
            }

            return cls;
        }

        [TestMethod]
        public void ShouldBeCreated()
        {
            Compiler compiler = new Compiler("x ^x");

            Assert.IsNotNull(compiler);
        }

        [TestMethod]
        public void ShouldCompileMethod()
        {
            PepsiMachine machine = new PepsiMachine();
            IClass cls = machine.CreateClass();
            cls.AddVariable("x");
            Compiler compiler = new Compiler("x [^x]");
            compiler.CompileInstanceMethod(cls);

            Assert.IsNotNull(cls.Lookup("x"));
        }

        [TestMethod]
        public void ShouldCompileMethodWithLocals()
        {
            PepsiMachine machine = new PepsiMachine();
            IClass cls = machine.CreateClass();
            cls.AddVariable("x");
            Compiler compiler = new Compiler("x [| temp | temp := x. ^temp]");
            compiler.CompileInstanceMethod(cls);

            Assert.IsNotNull(cls.Lookup("x"));
        }

        [TestMethod]
        public void ShouldCompileSetMethod()
        {
            PepsiMachine machine = new PepsiMachine();
            IClass cls = machine.CreateClass();
            cls.AddVariable("x");
            Compiler compiler = new Compiler("x: newX [x := newX]");
            compiler.CompileInstanceMethod(cls);

            Assert.IsNotNull(cls.Lookup("x:"));
        }

        [TestMethod]
        public void ShouldCompileSimpleCommand()
        {
            Compiler compiler = new Compiler("nil invokeWith: 10");
            Block block = compiler.CompileBlock();
            Assert.IsNotNull(block);
            Assert.AreEqual(0, block.NoLocals);
            Assert.IsNotNull(block.ByteCodes);
            Assert.AreEqual(0, block.Arity);
        }

        [TestMethod]
        public void ShouldCompileSubClassDefinition()
        {
            Compiler compiler = new Compiler("nil subclass: #Object");
            Block block = compiler.CompileBlock();
            Assert.IsNotNull(block);
            Assert.AreEqual(0, block.NoLocals);
            Assert.IsNotNull(block.ByteCodes);
            Assert.AreEqual(0, block.Arity);
        }

        [TestMethod]
        public void ShouldCompileSubClassDefinitionWithInstances()
        {
            Compiler compiler = new Compiler("nil subclass: #Object instanceVariables: 'a b c'");
            Block block = compiler.CompileBlock();
            Assert.IsNotNull(block);
            Assert.AreEqual(0, block.NoLocals);
            Assert.IsNotNull(block.ByteCodes);
            Assert.AreEqual(0, block.Arity);
        }

        [TestMethod]
        public void ShouldCompileTwoCommands()
        {
            Compiler compiler = new Compiler("nil invokeWith: 10. Global := 20");
            Block block = compiler.CompileBlock();
            Assert.IsNotNull(block);
            Assert.AreEqual(0, block.NoLocals);
            Assert.IsNotNull(block.ByteCodes);
            Assert.AreEqual(0, block.Arity);
        }

        [TestMethod]
        public void ShouldCompileBlock()
        {
            Compiler compiler = new Compiler("nil ifFalse: [self halt]");
            Block block = compiler.CompileBlock();

            Assert.IsNotNull(block);
            Assert.AreEqual(0, block.NoLocals);
            Assert.AreEqual(2, block.NoConstants);
            Assert.IsNotNull(block.ByteCodes);
            Assert.AreEqual(0, block.Arity);

            object constant = block.GetConstant(0);

            Assert.IsNotNull(constant);
            Assert.IsInstanceOfType(constant, typeof(Block));
        }

        [TestMethod]
        public void ShouldCompileEnclosedBlock()
        {
            Compiler compiler = new Compiler("[^0]");
            Block block = compiler.CompileEnclosedBlock();

            Assert.IsNotNull(block);
            Assert.AreEqual(0, block.NoLocals);
            Assert.AreEqual(1, block.NoConstants);
            Assert.IsNotNull(block.ByteCodes);
            Assert.AreEqual(0, block.Arity);

            object constant = block.GetConstant(0);

            Assert.IsNotNull(constant);
        }

        [TestMethod]
        public void ShouldCompileEnclosedEmptyBlock()
        {
            Compiler compiler = new Compiler("[]");
            Block block = compiler.CompileEnclosedBlock();

            Assert.IsNotNull(block);
            Assert.AreEqual(0, block.NoLocals);
            Assert.AreEqual(0, block.NoConstants);
            Assert.IsNull(block.ByteCodes);
            Assert.AreEqual(0, block.Arity);
        }

        [TestMethod]
        public void ShouldExecuteInstSize()
        {
            PepsiMachine machine = new PepsiMachine();

            machine.CreatePrototype("nil");
            object nil = machine.GetGlobalObject("nil");

            Assert.IsNotNull(nil);
            Assert.IsInstanceOfType(nil, typeof(IObject));

            Compiler compiler = new Compiler("^nil class basicNew instSize");
            Block block = compiler.CompileBlock();

            Assert.IsNotNull(block);

            object result = block.Execute(machine, null);

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void ShouldExecuteClass()
        {
            PepsiMachine machine = new PepsiMachine();

            object obj = machine.GetGlobalObject("Object");

            Assert.IsNotNull(obj);
            Assert.IsInstanceOfType(obj, typeof(IObject));

            Compiler compiler = new Compiler("^Object class basicNew class");
            Block block = compiler.CompileBlock();

            Assert.IsNotNull(block);

            object result = block.Execute(machine, null);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IClass));
        }

        [TestMethod]
        public void ShouldAddMethod()
        {
            PepsiMachine machine = new PepsiMachine();

            object obj = machine.GetGlobalObject("Object");

            Assert.IsNotNull(obj);
            Assert.IsInstanceOfType(obj, typeof(IObject));

            Compiler compiler = new Compiler("^Object class methodAt: #newMethod put: [self instSize]");
            Block block = compiler.CompileBlock();

            Assert.IsNotNull(block);

            block.Execute(machine, null);

            IObject iobj = (IObject)obj;

            Assert.IsNotNull(((IClass) iobj.Behavior).Lookup("newMethod"));
        }

        [TestMethod]
        public void ShouldExecuteInstSizeInRectangle()
        {
            PepsiMachine machine = new PepsiMachine();

            string[] methods = 
                {
                    "x [^x]",
                    "x: newX [x := newX]",
                    "y [^y]",
                    "y: newY [y := newY]"
                };

            IClass cls = CompileClass(
                "Rectangle", 
                new string[] { "x", "y" },
                methods);

            machine.SetGlobalObject("aRectangle", cls.CreateInstance());

            Compiler compiler = new Compiler("^aRectangle instSize");
            Block block = compiler.CompileBlock();

            Assert.IsNotNull(block);

            object result = block.Execute(machine, null);

            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void ShouldExecuteInstAt()
        {
            PepsiMachine machine = new PepsiMachine();

            string[] methods = 
                {
                    "x [^x]",
                    "x: newX [x := newX]",
                    "y [^y]",
                    "y: newY [y := newY]"
                };

            IClass cls = CompileClass(
                "Rectangle", 
                new string[] { "x", "y" },
                methods);

            IObject iobj = cls.CreateInstance();

            machine.SetGlobalObject("aRectangle", iobj);

            iobj.SetValueAt(0, 100);

            Compiler compiler = new Compiler("^aRectangle instAt: 0");
            Block block = compiler.CompileBlock();

            Assert.IsNotNull(block);

            object result = block.Execute(machine, null);

            Assert.AreEqual(100, result);
        }

        [TestMethod]
        public void ShouldExecuteInstAtPut()
        {
            PepsiMachine machine = new PepsiMachine();

            string[] methods = 
                {
                    "x [^x]",
                    "x: newX [x := newX]",
                    "y [^y]",
                    "y: newY [y := newY]"
                };            

                IClass cls = CompileClass(
                "Rectangle", 
                new string[] { "x", "y" },
                methods);

            IObject iobj = cls.CreateInstance();

            machine.SetGlobalObject("aRectangle", iobj);

            Compiler compiler = new Compiler("aRectangle instAt: 0 put: 200");
            Block block = compiler.CompileBlock();

            Assert.IsNotNull(block);

            block.Execute(machine, null);

            Assert.AreEqual(200, iobj.GetValueAt(0));
            Assert.IsNull(iobj.GetValueAt(1));
        }

        [TestMethod]
        public void ShouldExecuteBasicNew()
        {
            PepsiMachine machine = new PepsiMachine();
            IClass cls = CompileClass(
                "Rectangle",
                new string[] { "x", "y" },
                null);

            machine.SetGlobalObject("Rectangle", cls.CreateInstance());

            Compiler compiler = new Compiler("^Rectangle class basicNew");
            Block block = compiler.CompileBlock();

            Assert.IsNotNull(block);

            object obj = block.Execute(machine, null);

            Assert.IsNotNull(obj);
            Assert.IsInstanceOfType(obj, typeof(IObject));
            Assert.AreEqual(cls, ((IObject)obj).Behavior);
        }

        [TestMethod]
        public void ShouldCompileMethods()
        {
            string[] methods =
                {
                    "x [^x]",
                    "x: newX [x := newX]",
                    "y [^y]",
                    "y: newY [y := newY]"
                };

            IClass cls = CompileClass(
                "Rectangle", 
                new string[] { "x", "y" },
                methods);

            Assert.IsNotNull(cls);

            Assert.IsNotNull(cls.Lookup("x"));
            Assert.IsNotNull(cls.Lookup("y"));
            Assert.IsNotNull(cls.Lookup("x:"));
            Assert.IsNotNull(cls.Lookup("y:"));
        }

        [TestMethod]
        public void ShouldRunMethods()
        {
            string[] methods =
                {
                    "x [^x]",
                    "x: newX [x := newX]",
                    "y [^y]",
                    "y: newY [y := newY]"
                };

            IClass cls = CompileClass(
                "Rectangle", 
                new string[] { "x", "y" },
                methods);

            Assert.IsNotNull(cls);

            IObject obj = cls.CreateInstance();

            obj.Send("x:", 10);

            Assert.AreEqual(10, obj.GetValueAt(0));

            obj.Send("y:", 20);

            Assert.AreEqual(20, obj.GetValueAt(1));

            Assert.AreEqual(10, cls.Lookup("x").Execute(obj));
            Assert.AreEqual(20, cls.Lookup("y").Execute(obj));
        }

        [TestMethod]
        public void ShouldCompileMultiCommandMethod()
        {
            string[] methods =
                {
                    "side: newSide [x := newSide. y := newSide]"
                };

            IClass cls = CompileClass(
                "Rectangle", 
                new string[] { "x", "y" },
                methods);

            Assert.IsNotNull(cls);

            Assert.IsNotNull(cls.Lookup("side:"));
        }

        [TestMethod]
        public void ShouldCompileMultiCommandMethodWithLocal()
        {
            string[] methods =
                {
                    "side: newSide [| temp | temp := x. x := temp. y := temp]"
                };

            IClass cls = CompileClass(
                "Rectangle", 
                new string[] { "x", "y" },
                methods);

            Assert.IsNotNull(cls);

            Assert.IsNotNull(cls.Lookup("side:"));
        }

        [TestMethod]
        public void ShouldRunMultiCommandMethod()
        {
            string[] methods =
                {
                    "side: newSide [x := newSide. y := newSide]"
                };

            IClass cls = CompileClass(
                "Rectangle", 
                new string[] { "x", "y" },
                methods);

            Assert.IsNotNull(cls);

            IObject obj = cls.CreateInstance();

            obj.Send("side:", 10);

            Assert.AreEqual(10, obj.GetValueAt(0));
            Assert.AreEqual(10, obj.GetValueAt(1));
        }

        [TestMethod]
        public void ShouldRunMultiCommandMethodWithLocal()
        {
            string[] methods =
                {
                    "side: newSide [| temp | temp := newSide. x := temp. y := temp ]"
                };

            IClass cls = CompileClass(
                "Rectangle", 
                new string[] { "x", "y" },
                methods);

            Assert.IsNotNull(cls);

            IObject obj = cls.CreateInstance();

            obj.Send("side:", 10);

            Assert.AreEqual(10, obj.GetValueAt(0));
            Assert.AreEqual(10, obj.GetValueAt(1));
        }
    }
}

