namespace AjGroups
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Element : AjGroups.IElement
    {
        private byte[] values;
        private int calculatedOrder;

        public Element(params byte[] values)
        {
            for (int k = 0; k < values.Length; k++)
            {
                if (values[k] > values.Length) 
                {
                    throw new InvalidOperationException(string.Format("Invalid value {0}", values[k]));
                }

                for (int j = k + 1; j < values.Length; j++)
                {
                    if (values[k] == values[j])
                    {
                        throw new InvalidOperationException(string.Format("Repeated value {0}", values[k]));
                    }
                }
            }

            this.values = (byte[])values.Clone();
        }

        public int Size
        {
            get
            {
                return this.values.Length;
            }
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

        internal byte[] Values
        {
            get
            {
                return this.values;
            }
        }

        public static Element CreateIdentity(int size)
        {
            byte[] values = new byte[size];

            for (int k = 0; k < size; k++)
            {
                values[k] = (byte) k;
            }

            return new Element(values);
        }

        public static Element CreateSwap(int size) 
        {
            Element element = CreateIdentity(size);
            element.values[0] = 1;
            element.values[1] = 0;

            return element;
        }

        public static Element CreateSwap(int size, int position)
        {
            Element element = CreateIdentity(size);
            element.values[position] = (byte) (position+1);
            element.values[position+1] = (byte) position;

            return element;
        }

        public static Element CreateRotation(int size)
        {
            byte[] values = new byte[size];

            for (int k = 0; k < size; k++)
            {
                values[k] = (byte)(k == size - 1 ? 0 : k + 1);
            }

            return new Element(values);
        }

        public int CompareTo(IElement element)
        {
            if (this.Equals(element))
                return 0;

            if (this.Order < element.Order)
                return -1;

            if (this.Order > element.Order)
                return 1;

            if (!(element is Element))
                throw new InvalidOperationException("Element is not comparable");

            Element elem = (Element)element;

            for (int k = 0; k < this.Size; k++)
            {
                if (this.Values[k] < elem.Values[k])
                {
                    return -1;
                }

                if (this.Values[k] > elem.Values[k])
                {
                    return 1;
                }
            }

            return 0;
        }

        public override bool Equals(object obj)
        {
            if (this == obj)
            {
                return true;
            }

            if (!(obj is Element))
            {
                return false;
            }

            Element element = (Element)obj;

            if (this.values.Length != element.values.Length)
            {
                if (this.values.Length > element.values.Length)
                    return element.Equals(this);
            }

            if (this.Order != element.Order)
                return false;

            for (int k = 0; k < this.values.Length; k++)
                if (this.values[k] != element.values[k])
                    return false;

            for (int k = this.values.Length; k < element.values.Length; k++)
                if (element.values[k] != k)
                    return false;

            return true;
        }

        public override int GetHashCode()
        {
            int hash = 0;

            for (int k = this.values.Length-1; k > 0; k--)
            {
                hash *= 17;
                hash += this.values[k]-k;
            }

            return hash;
        }

        public IElement Multiply(IElement element)
        {
            return this.Multiply((Element)element);
        }

        public Element Multiply(Element element)
        {
            int k;
            int length1 = this.values.Length;
            int length2 = element.values.Length;
            int newlength = Math.Max(length1, length2);
            byte[] newvalues = new byte[newlength];

            for (k = 0; k < newlength; k++)
            {
                if (k >= length2)
                {
                    newvalues[k] = this.values[k];
                }
                else if (element.values[k] >= length1)
                {
                    newvalues[k] = element.values[k];
                }
                else
                {
                    newvalues[k] = this.values[element.values[k]];
                }
            }

            return new Element(newvalues);
        }

        public bool IsIdentity
        {
            get
            {
                for (int k = 0; k < this.values.Length; k++)
                {
                    if (this.values[k] != k)
                    {
                        return false;
                    }
                }

                return true;
            }
        }
    }
}
