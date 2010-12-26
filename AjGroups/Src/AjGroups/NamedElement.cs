namespace AjGroups
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class NamedElement : IElement
    {
        private char name;
        private OperationTable table;
        private int calculatedOrder;

        public NamedElement(char name)
        {
            this.name = name;
        }

        public NamedElement(int n)
        {
            if (n == 0)
                this.name = 'e';
            if (n > 4)
                this.name = (char)(n + 'a');
            if (n > 0 && n <= 4)
                this.name = (char)(n - 1 + 'a');
        }

        public int Order
        {
            get
            {
                if (this.calculatedOrder > 0)
                {
                    return this.calculatedOrder;
                }

                IElement element = this;

                while (!element.IsIdentity)
                {
                    element = this.Multiply(element);
                    this.calculatedOrder++;
                }

                this.calculatedOrder++;

                return this.calculatedOrder;
            }
        }

        public char Name { get { return this.name; } }

        public OperationTable OperationTable
        {
            get
            {
                return this.table;
            }

            set
            {
                this.table = value;
            }
        }

        public IElement Multiply(IElement element)
        {
            return this.table.GetValue(this, element);
        }

        public int CompareTo(IElement other)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            if (this == obj)
            {
                return true;
            }

            if (!(obj is NamedElement))
            {
                return false;
            }

            NamedElement element = (NamedElement)obj;

            return this.name == element.name && this.table == element.table;
        }

        public override int GetHashCode()
        {
            return this.name.GetHashCode();
        }

        public bool IsIdentity
        {
            get
            {
                return this.name == 'e';
            }
        }
    }
}
