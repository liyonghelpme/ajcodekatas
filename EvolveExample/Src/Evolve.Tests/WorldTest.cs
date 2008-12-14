using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

using Evolve;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Evolve.Tests
{
    /// <summary>
    /// Summary description for WorldTest
    /// </summary>
    [TestClass]
    public class WorldTest
    {
        [TestMethod]
        public void ShouldCreate()
        {
            World world = new World(10, 10, 1000, 10, 100);

            Assert.IsNotNull(world);
            Assert.IsNotNull(world.Field);
            Assert.IsNotNull(world.Animals);

            Assert.AreEqual(10, world.Field.Width);
            Assert.AreEqual(10, world.Field.Height);

            Assert.AreEqual(10, world.Animals.Count);
            Assert.AreEqual(100, world.Animals[0].Energy);
        }

        [TestMethod]
        public void ShouldGetBestSoFar()
        {
            World world = new World(10, 10, 1000, 10, 100);

            for (int k = 0; k < 100; k++)
                world.RunStep();

            Animal best = world.BestSoFar();

            foreach (Animal animal in world.Animals)
                Assert.IsTrue(best.Energy >= animal.Energy);

            world.Evolve();

            best = world.BestSoFar();

            foreach (Animal animal in world.Animals)
                Assert.IsTrue(best.Energy >= animal.Energy);

            world.Evolve();
        }
    }
}
