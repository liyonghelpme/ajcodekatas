namespace Evolve
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class World
    {
        private static AnimalComparer comparer = new AnimalComparer();
        private static Random random = new Random();

        private List<Animal> animals;
        private int food;
        private int energy;

        public World(int width, int height, int food, int nanimals, int energy)
        {
            this.food = food;
            this.energy = energy;
            this.Field = new Field(width, height);
            this.Field.SeedFood(food);
            this.animals = new List<Animal>();

            for (int k = 0; k < nanimals; k++)
            {
                this.animals.Add(new Animal(this, energy));
            }
        }

        public Field Field 
        { 
            get; 
            private set; 
        }

        public List<Animal> Animals 
        { 
            get 
            { 
                return this.animals; 
            } 
        } 

        public void RunStep()
        {
            foreach (Animal animal in this.animals)
            {
                animal.DoStep();
            }
        }

        public void Reset()
        {
            this.Field.ClearFood();
            this.Field.SeedFood(this.food);

            foreach (Animal animal in this.Animals)
            {
                animal.Energy = this.energy;
            }
        }

        public Animal BestSoFar()
        {
            Animal best = null;

            foreach (Animal animal in this.animals)
            {
                if (best == null || best.Energy < animal.Energy)
                {
                    best = animal;
                }
            }

            return best;
        }

        public List<Animal> OrderedAnimals()
        {
            this.animals.Sort(comparer);

            return this.animals;
        }

        public void Evolve()
        {
            List<Animal> newanimals = new List<Animal>();

            this.animals.Sort(comparer);

            for (int k = 0; k < this.animals.Count / 3; k++)
            {
                newanimals.Add(this.animals[k]);
            }

            for (int k = newanimals.Count; k < this.animals.Count - (animals.Count * 2 / 3); k++)
            {
                newanimals.Add(Mutate(newanimals[random.Next(newanimals.Count)]));
            }

            for (int k = newanimals.Count; k < animals.Count; k++)
            {
                newanimals.Add(new Animal(this.Field, this.energy));
            }

            this.animals = newanimals;
        }

        public Animal GetVictim(Animal predator)
        {
            foreach (Animal animal in animals)
            {
                if (animal.XPosition == predator.XPosition && animal.YPosition == predator.YPosition && !animal.Equals(predator))
                {
                    return animal;
                }
            }

            return null;
        }

        private Animal Mutate(Animal animal)
        {
            List<Instruction> newprogram = new List<Instruction>(animal.Program);

            int option = random.Next(3);

            switch (option)
            {
                case 0: // Double mutate
                    return Mutate(Mutate(animal));
                    break;
                case 1: // Add instruction
                    Instruction newinstruction = Utilities.GenerateInstruction();

                    int newposition = random.Next(newprogram.Count + 1);

                    if (newposition >= newprogram.Count)
                    {
                        newprogram.Add(newinstruction);
                    }
                    else
                    {
                        newprogram.Insert(newposition, newinstruction);
                    }

                    break;
                case 2: // Remove instruction
                    if (newprogram.Count > 0)
                    {
                        newprogram.RemoveAt(random.Next(newprogram.Count));
                    } 

                    break;
            }

            return new Animal(this.Field, this.energy, newprogram);
        }
    }

    internal class AnimalComparer : Comparer<Animal>
    {
        public override int Compare(Animal x, Animal y)
        {
            if (x.Energy > y.Energy)
            {
                return -1;
            }

            if (x.Energy < y.Energy)
            {
                return 1;
            }

            return 0;
        }
    }
}
