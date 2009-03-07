using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AjProcessor.Tests
{
    internal class TestObject
    {
        public string ProcessedString { get; set; }
        public int ProcessedInteger { get; set; }

        public void Process(string value)
        {
            this.ProcessedString = value;
        }

        private void PrivateProcess(string value)
        {
            this.ProcessedString = value;
        }

        public int Increment(int value)
        {
            this.ProcessedInteger = value;

            return value + 1;
        }
    }
}
