using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AjIo.Console
{
    class ConsoleTextReader : TextReader
    {
        private string line;
        private int position;

        public override int Read()
        {
            if (this.line == null)
            {
                System.Console.Write("AjIo>");
                System.Console.Out.Flush();
                this.line = System.Console.ReadLine();
                this.position = 0;
            }

            if (this.position >= this.line.Length)
            {
                this.line = null;
                return '\n';
            }

            return this.line[this.position++];
        }
    }
}
