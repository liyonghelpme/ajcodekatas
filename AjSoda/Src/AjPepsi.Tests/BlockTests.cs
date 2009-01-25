namespace AjPepsi.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AjPepsi;
    using AjSoda;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BlockTests
    {
        [TestMethod]
        public void ShouldBeCreated()
        {
            IBlock block = new Block();

            Assert.IsNotNull(block);
        }

        [TestMethod]
        public void ShouldCompile()
        {
            PepsiMachine machine = new PepsiMachine();
            IClass cls = machine.CreateClass();
            cls.AddVariable("x");

            Block block;

            block = new Block();
            block.CompileArgument("newX");
            block.CompileGet("newX");
            block.CompileSet("x");

            Assert.AreEqual(1, block.Arity);
            Assert.AreEqual(0, block.NoLocals);
            Assert.IsNotNull(block.ByteCodes);
            Assert.IsTrue(block.ByteCodes.Length > 0);
        }

        [TestMethod]
        public void ShouldCompileGlobal()
        {
            Block block;

            block = new Block();
            block.CompileGetConstant(10);
            block.CompileSet("Global");

            Assert.AreEqual("Global", block.GetGlobalName(0));
        }

        [TestMethod]
        public void ShouldCompileAndExecuteGetDotNetType()
        {
            Block block;

            block = new Block();
            block.CompileGetDotNetType("System.IO.FileInfo");
            block.CompileByteCode(ByteCode.ReturnPop);

            object obj = block.Execute(null);

            Assert.IsNotNull(obj);
            Assert.IsInstanceOfType(obj, typeof(System.Type));
        }

        [TestMethod]
        public void ShouldCompileAndExecuteNewDotNetObject()
        {
            Block block;

            block = new Block();
            block.CompileGetDotNetType("System.IO.FileInfo");
            block.CompileGetConstant("FooBar.txt");
            block.CompileSend("!new:");
            block.CompileByteCode(ByteCode.ReturnPop);

            object obj = block.Execute(null);

            Assert.IsNotNull(obj);
            Assert.IsInstanceOfType(obj, typeof(System.IO.FileInfo));
        }

        [TestMethod]
        public void ShouldCompileAndRunWithGlobal()
        {
            Block block;

            block = new Block();
            block.CompileGetConstant(10);
            block.CompileSet("Global");

            PepsiMachine machine = new PepsiMachine();

            block.Execute(machine);

            Assert.AreEqual(10, machine.GetGlobalObject("Global"));
        }

        [TestMethod]
        public void ShouldCompileWithLocals()
        {
            PepsiMachine machine = new PepsiMachine();
            IClass cls = machine.CreateClass();
            cls.AddVariable("x");

            Block block;

            block = new Block();
            block.CompileArgument("newX");
            block.CompileLocal("l");
            block.CompileGet("newX");
            block.CompileSet("l");

            Assert.AreEqual(1, block.NoLocals);
            Assert.IsNotNull(block.ByteCodes);
            Assert.IsTrue(block.ByteCodes.Length > 0);
        }

        [TestMethod]
        public void ShouldCompileAndRun()
        {
            PepsiMachine machine = new PepsiMachine();

            Block block;

            block = new Block();
            block.CompileArgument("newX");
            block.CompileGet("newX");
            block.CompileSet("GlobalX");

            block.Execute(machine, 10);

            Assert.AreEqual(10, machine.GetGlobalObject("GlobalX"));
        }

        [TestMethod]
        public void ShouldCompileWithLocalsAndRun()
        {
            PepsiMachine machine = new PepsiMachine();

            Block block;

            block = new Block();
            block.CompileArgument("newX");
            block.CompileLocal("l");
            block.CompileGet("newX");
            block.CompileSet("l");
            block.CompileGet("l");
            block.CompileSet("GlobalX");

            block.Execute(machine, new object[] { 10 });

            Assert.AreEqual(10, machine.GetGlobalObject("GlobalX"));
        }

        [TestMethod]
        public void ShouldCompileGlobalName()
        {
            Block block = new Block();
            byte p = block.CompileGlobal("Class");

            Assert.AreEqual(0, p);
        }

        [TestMethod]
        public void ShouldCompileGlobalNames()
        {
            Block block = new Block();
            byte p1 = block.CompileGlobal("Class");
            byte p2 = block.CompileGlobal("Object");
            byte p3 = block.CompileGlobal("Class");

            Assert.AreEqual(0, p1);
            Assert.AreEqual(1, p2);
            Assert.AreEqual(0, p3);
        }

        [TestMethod]
        public void ShouldCompileGetGlobalVariable()
        {
            Block block = new Block();
            block.CompileGet("Class");
            block.CompileGet("Object");

            byte p1 = block.CompileGlobal("Object");
            byte p2 = block.CompileGlobal("Class");

            Assert.AreEqual(1, p1);
            Assert.AreEqual(0, p2);
        }

        [TestMethod]
        public void ShouldCreateObjectInstance()
        {
            Block block = new Block();

            block.CompileGet("Object");
            block.CompileSend("class");
            block.CompileSend("basicNew");
            block.CompileReturnPop();

            PepsiMachine machine = new PepsiMachine();

            object obj = block.Execute(machine, null);

            Assert.IsNotNull(obj);
            Assert.IsInstanceOfType(obj, typeof(IObject));

            IObject obj2 = (IObject)obj;

            Assert.IsNotNull(obj2.Behavior);

            IObject obj3 = (IObject) machine.GetGlobalObject("Object");
            Assert.AreEqual(obj2.Behavior, obj3.Behavior);
            Assert.AreEqual(0, obj2.Size);
        }

        [TestMethod]
        public void ShouldCreateSubclass()
        {
            Block block = new Block();

            block.CompileGet("Object");
            block.CompileSend("class");
            block.CompileSend("delegated");
            block.CompileReturnPop();

            object obj = block.Execute(new PepsiMachine(), null);

            Assert.IsNotNull(obj);
            Assert.IsInstanceOfType(obj, typeof(IClass));

            IClass cls = (IClass)obj;

            Assert.AreEqual(0, cls.InstanceSize);
        }

        [TestMethod]
        public void ShouldDefineSubclass()
        {
            Block block = new Block();

            block.CompileGet("Object");
            block.CompileSend("class");
            block.CompileSend("delegated");
            block.CompileSet("NewClass");

            PepsiMachine machine = new PepsiMachine();

            block.Execute(machine, null);

            object obj = machine.GetGlobalObject("NewClass");

            Assert.IsNotNull(obj);
            Assert.IsInstanceOfType(obj, typeof(IObject));

            IObject iobj = (IObject)obj;
            IClass cls = (IClass)iobj.Behavior;

            Assert.AreEqual(0, cls.InstanceSize);
        }

        [TestMethod]
        public void ShouldCalculateArity()
        {
            Assert.AreEqual(0, Block.MessageArity("class"));
            Assert.AreEqual(1, Block.MessageArity("with:"));
            Assert.AreEqual(2, Block.MessageArity("with:with:"));
        }
    }
}

