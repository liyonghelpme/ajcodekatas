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
            if (id == null)
                throw new ArgumentNullException("id");

            lock (this)
            {
                if (items.ContainsKey(id))
                    throw new InvalidOperationException(string.Format("Item {0} already exists", id));

                Item item = new Item(id, this);
                items[id] = item;

                return item;
            }
        }

        public Item GetItem(object id)
        {
            if (id == null)
                throw new ArgumentNullException("id");

            lock (this)
            {
                if (!items.ContainsKey(id))
                    throw new InvalidOperationException(string.Format("Unknown Item with Id '{0}'", id.ToString()));

                return items[id];
            }
        }

        public bool HasItem(object id)
        {
            if (id == null)
                throw new ArgumentNullException("id");

            lock (this)
            {
                return this.items.ContainsKey(id);
            }
        }

        public void RemoveItem(object id)
        {
            if (id == null)
                throw new ArgumentNullException("id");

            lock (this)
            {
                if (!items.ContainsKey(id))
                    throw new InvalidOperationException(string.Format("Unknown Item with Id '{0}'", id.ToString()));

                items.Remove(id);
            }
        }

        public IEnumerable<Item> GetItems(Predicate<Item> filter)
        {
            lock (this)
            {
                if (filter == null)
                    return new List<Item>(from v in this.items.Values select v.CloneItem());

                return new List<Item>(from v in this.items.Values where filter(v) select v.CloneItem());
            }
        }
    }
}
