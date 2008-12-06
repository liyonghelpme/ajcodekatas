using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

using AjProlog.Core;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AjProlog.Tests
{
    /// <summary>
    /// Summary description for PrimitivesTest
    /// </summary>
    [TestClass]
    public class PrimitivesTest
    {
        [TestMethod]
        public void AtomicPrimitiveAcceptsAtom()
        {
            StructureObject so = new StructureObject("atomic", "a");
            PrologMachine pm = new PrologMachine();

            Assert.IsTrue(pm.Resolve(so));
        }

        [TestMethod]
        public void AtomicPrimitiveAcceptsInteger()
        {
            StructureObject so = new StructureObject("atomic", 1);
            PrologMachine pm = new PrologMachine();

            Assert.IsTrue(pm.Resolve(so));
        }

        [TestMethod]
        public void AtomicPrimitiveRejectsStructure()
        {
            StructureObject so = new StructureObject("atomic", new StructureObject("foo","bar"));
            PrologMachine pm = new PrologMachine();

            Assert.IsFalse(pm.Resolve(so));
        }

        [TestMethod]
        public void IntegerPrimitiveRejectsAtom()
        {
            StructureObject so = new StructureObject("integer", "a");
            PrologMachine pm = new PrologMachine();

            Assert.IsFalse(pm.Resolve(so));
        }

        [TestMethod]
        public void IntegerPrimitiveAcceptsInteger()
        {
            StructureObject so = new StructureObject("integer", 1);
            PrologMachine pm = new PrologMachine();

            Assert.IsTrue(pm.Resolve(so));
        }

        [TestMethod]
        public void IntegerPrimitiveRejectsStructure()
        {
            StructureObject so = new StructureObject("integer", new StructureObject("foo", "bar"));
            PrologMachine pm = new PrologMachine();

            Assert.IsFalse(pm.Resolve(so));
        }

        [TestMethod]
        public void VarPrimitiveRejectsAtom()
        {
            StructureObject so = new StructureObject("var", "a");
            PrologMachine pm = new PrologMachine();

            Assert.IsFalse(pm.Resolve(so));
        }

        [TestMethod]
        public void VarPrimitiveRejectsInteger()
        {
            StructureObject so = new StructureObject("var", 1);
            PrologMachine pm = new PrologMachine();

            Assert.IsFalse(pm.Resolve(so));
        }

        [TestMethod]
        public void VarPrimitiveRejectsStructure()
        {
            StructureObject so = new StructureObject("var", new StructureObject("foo", "bar"));
            PrologMachine pm = new PrologMachine();

            Assert.IsFalse(pm.Resolve(so));
        }

        [TestMethod]
        public void VarPrimitiveAcceptsVariable()
        {
            StructureObject so = new StructureObject("var", "X");
            PrologMachine pm = new PrologMachine();

            Assert.IsTrue(pm.Resolve(so));
        }

        [TestMethod]
        public void NonVarPrimitiveAcceptsAtom()
        {
            StructureObject so = new StructureObject("nonvar", "a");
            PrologMachine pm = new PrologMachine();

            Assert.IsTrue(pm.Resolve(so));
        }

        [TestMethod]
        public void NonVarPrimitiveAcceptsInteger()
        {
            StructureObject so = new StructureObject("nonvar", 1);
            PrologMachine pm = new PrologMachine();

            Assert.IsTrue(pm.Resolve(so));
        }

        [TestMethod]
        public void NonVarPrimitiveAcceptsStructure()
        {
            StructureObject so = new StructureObject("nonvar", new StructureObject("foo", "bar"));
            PrologMachine pm = new PrologMachine();

            Assert.IsTrue(pm.Resolve(so));
        }

        [TestMethod]
        public void NonVarPrimitiveRejectsVariable()
        {
            StructureObject so = new StructureObject("nonvar", "X");
            PrologMachine pm = new PrologMachine();

            Assert.IsFalse(pm.Resolve(so));
        }

        [TestMethod]
        public void EqualPrimitiveUnifyAtomWithVariable()
        {
            StructureObject so = new StructureObject("=", "a", "X");
            PrologMachine pm = new PrologMachine();

            Assert.IsTrue(pm.Resolve(so));
            Assert.AreEqual(so.Parameters[0], pm.GetVariable(0).Dereference());
        }

        [TestMethod]
        public void EqualPrimitiveUnifyVariables()
        {
            StructureObject so = new StructureObject("=", "Y", "X");
            PrologMachine pm = new PrologMachine();

            Assert.IsTrue(pm.Resolve(so));
        }

        [TestMethod]
        public void EqualPrimitiveDontUnifyDifferentAtoms()
        {
            StructureObject so = new StructureObject("=", "a", "b");
            PrologMachine pm = new PrologMachine();

            Assert.IsFalse(pm.Resolve(so));
        }

        [TestMethod]
        public void EqualPrimitiveUnifyInternalAtomVariable()
        {
            StructureObject so = new StructureObject("=", new StructureObject("f", "a"), new StructureObject("f", "X"));
            PrologMachine pm = new PrologMachine();

            Assert.IsTrue(pm.Resolve(so));
            Assert.AreEqual("a", pm.GetVariable(0).Dereference().ToString());
        }
    }
}

