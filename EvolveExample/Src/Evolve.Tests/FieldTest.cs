namespace Evolve.Tests
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;

    using Evolve;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Summary description for FieldTest
    /// </summary>
    [TestClass]
    public class FieldTest
    {
        [TestMethod]
        public void ShouldCreateField()
        {
            Field field = new Field(10, 20);

            Assert.IsNotNull(field);
            Assert.AreEqual(10, field.Width);
            Assert.AreEqual(20, field.Height);

            for (int x = 0; x < 10; x++)
                for (int y = 0; y < 20; y++)
                    Assert.AreEqual(0, field.GetFoodAt(x, y));

            Assert.AreEqual(0, field.FoodLevel);
        }

        [TestMethod]
        public void ShouldSeedFood()
        {
            Field field = new Field(10, 10);

            field.SeedFood(1000);

            Assert.AreEqual(1000, field.FoodLevel);
        }
    }
}
