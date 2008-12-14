using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

using Evolve;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Evolve.Tests
{
    /// <summary>
    /// Summary description for AnimalTest
    /// </summary>
    [TestClass]
    public class AnimalTest
    {
        [TestMethod]
        public void ShouldCreateAnimalInField()
        {
            Animal animal = new Animal(new Field(10, 10), 100);

            Assert.IsNotNull(animal);
            Assert.IsNotNull(animal.Field);
            Assert.AreEqual(100, animal.Energy);

            Assert.IsNotNull(animal.Program);
            Assert.IsTrue(animal.Program.Count > 0);

            Assert.IsTrue(animal.XPosition < animal.Field.Width);
            Assert.IsTrue(animal.YPosition < animal.Field.Height);
        }

        [TestMethod]
        public void ShouldDoEatStep()
        {
            List<Instruction> program = new List<Instruction>();
            program.Add(Instruction.Eat);

            Animal animal = new Animal(new Field(10, 10), 100, program);

            animal.DoStep();

            Assert.AreEqual("Eat", animal.ProgramText);
        }

        [TestMethod]
        public void ShouldEat()
        {
            Animal animal = new Animal(new Field(10, 10), 100);

            int food = animal.Field.GetFoodAt(animal.XPosition, animal.YPosition);
            int energy = animal.Energy;

            animal.Eat(food);

            Assert.AreEqual(0, animal.Field.GetFoodAt(animal.XPosition, animal.YPosition));
            Assert.AreEqual(energy + food, animal.Energy);
        }

        [TestMethod]
        public void ShouldMoveWest()
        {
            List<Instruction> program = new List<Instruction>();
            program.Add(Instruction.West);

            Animal animal = new Animal(new Field(10, 10), 100, program, 1, 0);

            animal.DoStep();

            Assert.AreEqual(0, animal.XPosition);
            Assert.AreEqual(0, animal.YPosition);
        }

        [TestMethod]
        public void ShouldMoveEast()
        {
            List<Instruction> program = new List<Instruction>();
            program.Add(Instruction.East);

            Animal animal = new Animal(new Field(10, 10), 100, program, 0, 0);

            animal.DoStep();

            Assert.AreEqual(1, animal.XPosition);
            Assert.AreEqual(0, animal.YPosition);
        }

        [TestMethod]
        public void ShouldMoveNorth()
        {
            List<Instruction> program = new List<Instruction>();
            program.Add(Instruction.North);

            Animal animal = new Animal(new Field(10, 10), 100, program, 0, 1);

            animal.DoStep();

            Assert.AreEqual(0, animal.XPosition);
            Assert.AreEqual(0, animal.YPosition);
        }

        [TestMethod]
        public void ShouldMoveSouth()
        {
            List<Instruction> program = new List<Instruction>();
            program.Add(Instruction.South);

            Animal animal = new Animal(new Field(10, 10), 100, program, 0, 0);

            animal.DoStep();

            Assert.AreEqual(0, animal.XPosition);
            Assert.AreEqual(1, animal.YPosition);
        }

        [TestMethod]
        public void ShouldMoveAndReturn()
        {
            List<Instruction> program = new List<Instruction>();
            program.Add(Instruction.South);
            program.Add(Instruction.West);

            Animal animal = new Animal(new Field(10, 10), 100, program, 0, 0);

            for (int k = 0; k < 10; k++)
                animal.DoStep();

            Assert.AreEqual(0, animal.XPosition);
            Assert.AreEqual(0, animal.YPosition);
        }
    }
}
