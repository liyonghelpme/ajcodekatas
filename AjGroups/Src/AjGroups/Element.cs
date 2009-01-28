namespace AjGroups
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Element
    {
        private byte[] values;

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
                return false;
            }

            for (int k = 0; k < this.values.Length; k++)
            {
                if (this.values[k] != element.values[k])
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            int hash = 0;

            for (int k = 0; k < this.values.Length; k++)
            {
                hash *= 17;
                hash += this.values[k];
            }

            return hash;
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

            //int j;

            //for (j = newlength - 1; j >= 0 && newvalues[j] == j; j--)
            //{
            //}

            //if (j + 1 < newlength)
            //{
            //    byte[] newvalues2 = new byte[j + 1];
            //    Array.Copy(newvalues, newvalues2, j + 1);
            //    newvalues = newvalues2;
            //}

            return new Element(newvalues);
        }
    }
}
