namespace AjGroups
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class OperationTable
    {
        private IList<IElement> elements;
        private IElement[,] cells;

        public OperationTable(ICollection<IElement> elements)
            : this(elements, false)
        {
        }

        public OperationTable(ICollection<IElement> elements, bool firstElementIsIdentity)
        {
            this.elements = new List<IElement>(elements);
            this.cells = new IElement[elements.Count, elements.Count];

            if (firstElementIsIdentity)
                for (int k = 0; k < this.elements.Count; k++)
                {
                    this.cells[k, 0] = this.elements[k];
                    this.cells[0, k] = this.elements[k];
                }
        }

        public OperationTable(OperationTable table)
        {
            this.elements = new List<IElement>(table.elements);
            this.cells = new IElement[this.elements.Count, this.elements.Count];
            Array.Copy(table.cells, this.cells, this.elements.Count * this.elements.Count);
        }

        public ICollection<IElement> Elements
        {
            get { return this.elements; }
        }

        public bool IsAssociative
        {
            get
            {
                foreach (IElement left in this.elements)
                    foreach (IElement middle in this.elements)
                        foreach (IElement right in this.elements)
                            if (!left.Multiply(middle.Multiply(right)).Equals((left.Multiply(middle).Multiply(right))))
                                return false;

                return true;
            }
        }

        public bool IsClosed
        {
            get
            {
                for (int k = 0; k < this.elements.Count; k++)
                    for (int j = 0; j < this.elements.Count; j++)
                        if (!this.elements.Contains(cells[k, j]))
                            return false;

                return true;
            }
        }

        public bool HasIdentity
        {
            get
            {
                foreach (IElement element in this.elements)
                    if (element.Order == 1)
                        return true;

                return false;
            }
        }

        public bool IsCommutative
        {
            get
            {
                for (int k = 1; k < this.elements.Count; k++)
                    for (int j = k + 1; j < this.elements.Count; j++)
                        if (!cells[k, j].Equals(cells[j, k]))
                            return false;

                return true;
            }
        }

        public void Calculate()
        {
            foreach (IElement left in this.elements)
                foreach (IElement right in this.elements)
                    this.SetValue(left, right, left.Multiply(right));
        }

        public void SetValue(IElement left, IElement right, IElement value)
        {
            this.SetValue(elements.IndexOf(left), elements.IndexOf(right), value);
        }

        public IElement GetValue(IElement left, IElement right)
        {
            return this.GetValue(elements.IndexOf(left), elements.IndexOf(right));
        }

        public OperationTable GetCompatibleTable(IElement left, IElement right, IElement value)
        {
            IElement actualvalue = this.GetValue(left, right);

            if (actualvalue == value)
                return this;

            if (actualvalue != null)
                return null;

            if (!this.elements.Contains(left))
                return null;

            if (!this.elements.Contains(right))
                return null;

            int x = this.elements.IndexOf(left);
            int y = this.elements.IndexOf(right);

            for (int k = 0; k < this.elements.Count; k++)
            {
                if (value.Equals(this.cells[k, y]))
                    return null;
                if (value.Equals(this.cells[x, k]))
                    return null;
            }

            OperationTable table = new OperationTable(this);
            
            // TODO complete other values, test compatibility
            table.SetValue(left, right, value);
            return table;
        }

        private void SetValue(int leftpos, int rightpos, IElement value)
        {
            cells[leftpos, rightpos] = value;
        }

        private IElement GetValue(int leftpost, int rightpos)
        {
            return cells[leftpost, rightpos];
        }
    }
}
