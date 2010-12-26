namespace AjGroups
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class TableGroup : IGroup
    {
        private List<IElement> elements;
        private OperationTable table;

        public TableGroup(OperationTable table)
        {
            this.elements = new List<IElement>();

            foreach (IElement element in table.Elements)
                this.elements.Add(new NamedElement(((NamedElement) element).Name));

            OperationTable newtable = new OperationTable(this.elements);

            int n = this.elements.Count;

            for (int k = 0; k < n; k++)
                for (int j = 0; j < n; j++) 
                {
                    IElement oldvalue = table.GetValue(k, j);
                    IElement newvalue = this.elements[this.elements.IndexOf(oldvalue)];
                    newtable.SetValue(k, j, newvalue);
                }

            foreach (IElement element in this.elements)
            {
                NamedElement nelement = (NamedElement) element;
                nelement.OperationTable = newtable;
            }

            this.table = newtable;
        }

        public OperationTable Table { get { return this.table; } }

        public List<IElement> Elements
        {
            get { return this.elements; }
        }

        public int Order
        {
            get { return this.elements.Count; }
        }
    }
}
