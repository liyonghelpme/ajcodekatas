using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evolve
{
    internal class Utilities
    {
        private static Random random = new Random();

        internal static Instruction GenerateInstruction()
        {
            return (Instruction)random.Next(6);
        }
    }
}
