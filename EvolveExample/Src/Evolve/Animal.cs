using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evolve
{
    public class Animal
    {
        private static Random random = new Random();

        private Field field;
        private World world;
        private int x;
        private int y;
        private List<Instruction> program;

        public Animal(Field field, int energy)
        {
            this.field = field;
            this.x = random.Next(field.Width);
            this.y = random.Next(field.Height);
            this.Energy = energy;
            this.CreateProgram();
        }

        public Animal(World world, int energy)
        {
            this.field = world.Field;
            this.world = world;
            this.x = random.Next(this.field.Width);
            this.y = random.Next(this.field.Height);
            this.Energy = energy;
            this.CreateProgram();
        }

        public Animal(Field field, int energy, List<Instruction> program)
        {
            this.field = field;
            this.x = random.Next(field.Width);
            this.y = random.Next(field.Height);
            this.Energy = energy;
            this.program = program;
        }

        public Animal(Field field, int energy, List<Instruction> program, int x, int y)
        {
            this.field = field;
            this.x = x;
            this.y = y;
            this.Energy = energy;
            this.program = program;
        }

        public int Energy { get; set; }

        public int XPosition 
        { 
            get 
            { 
                return this.x; 
            } 
        }

        public int YPosition
        { 
            get 
            { 
                return this.y; 
            } 
        }

        public Field Field 
        { 
            get 
            { 
                return this.field; 
            } 
        }

        public List<Instruction> Program 
        { 
            get 
            { 
                return program; 
            } 
        }

        public void DoStep()
        {
            foreach (Instruction instruction in program)
            {
                if (this.Energy <= 0)
                {
                    return;
                }

                DoInstruction(instruction);
                this.Energy--;
            }
        }

        public void DoInstruction(Instruction instruction)
        {
            switch (instruction)
            {
                case Instruction.Eat:
                    this.Eat(10);
                    break;
                case Instruction.North:
                    this.Move(0, -1);
                    break;
                case Instruction.South:
                    this.Move(0, 1);
                    break;
                case Instruction.West:
                    this.Move(-1, 0);
                    break;
                case Instruction.East:
                    this.Move(1, 0);
                    break;
                case Instruction.Predate:
                    if (this.world == null)
                    {
                        break;
                    }

                    Animal victim = this.world.GetVictim(this);

                    if (victim == null)
                    {
                        break;
                    }

                    int energy = Math.Min(100,victim.Energy);

                    victim.Energy -= energy;
                    this.Energy += energy;

                    break;                        
            }
        }

        public void Eat(int n)
        {
            int food = field.GetFoodAt(this.x, this.y);

            if (food >= n)
                food = n;

            field.EatFood(food, this.x, this.y);

            this.Energy += food;
        }

        public string ProgramText
        {
            get
            {
                string text = "";

                foreach (Instruction instruction in this.program)
                    text += instruction.ToString();

                return text;
            }
        }

        public void Move(int dx, int dy)
        {
            x += dx;
            y += dy;

            if (x < 0)
            {
                x += field.Width;
            }

            if (y < 0)
            {
                y += field.Height;
            }

            if (x >= field.Width)
            {
                x -= field.Width;
            }

            if (y >= field.Height)
            {
                y -= field.Height;
            }
        }

        private void CreateProgram()
        {
            int length = random.Next(5) + 1;

            program = new List<Instruction>();

            for (int k = 0; k < length; k++)
                program.Add(Utilities.GenerateInstruction());
        }
    }
}

