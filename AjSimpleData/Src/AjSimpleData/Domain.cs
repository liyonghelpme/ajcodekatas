namespace AjSimpleData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Domain
    {
        private Container container;
        private string name;
        private Dictionary<object, Item> items = new Dictionary<object, Item>();

        internal Domain(string name, Container container)
        {
            this.name = name;
            this.container = container;
        }

        public string Name { get { return this.name; } }

        public Container Container { get { return this.container; } }

        public Item CreateItem(object id)
        {
            if (items.ContainsKey(id))
                throw new InvalidOperationException(string.Format("Item {0} already exists", id));

            Item item = new Item(id, this);
            items[id] = item;

            return item;
        }

        public Item GetItem(object id)
        {
            return items[id];
        }

        public IEnumerable<Item> GetItems(Predicate<Item> filter)
        {
            if (filter == null)
                return this.items.Values;

            return from v in this.items.Values where filter(v) select v;
        }
    }
}
