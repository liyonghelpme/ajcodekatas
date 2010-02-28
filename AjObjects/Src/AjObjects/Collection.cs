namespace AjObjects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Collection
    {
        private Dictionary<System.Guid, BasicObject> index = new Dictionary<Guid, BasicObject>();
        private List<BasicObject> objects = new List<BasicObject>();

        public void Insert(BasicObject obj)
        {
            lock (this)
            {
                if (obj["_id"] == null)
                    obj["_id"] = Guid.NewGuid();

                Guid id = (Guid)obj["_id"];

                BasicObject clone = obj.MakeDeepCopy();
                this.index[id] = clone;
                this.objects.Add(clone);
            }
        }

        public BasicObject GetObject(Guid id)
        {
            lock (this)
            {
                if (!this.index.ContainsKey(id))
                    return null;

                return this.index[id].MakeDeepCopy();
            }
        }

        public Cursor FindAll()
        {
            lock (this)
            {
                return new Cursor(this.objects);
            }
        }

        public Cursor Find(Predicate<BasicObject> filter)
        {
            lock (this)
            {
                return new Cursor(from obj in this.objects where filter(obj) select obj);
            }
        }

        public void DeleteObject(Guid id)
        {
            lock (this)
            {
                if (!this.index.ContainsKey(id))
                    return;

                BasicObject obj = this.index[id];
                this.index.Remove(id);
                this.objects.Remove(obj);
            }
        }

        public void Delete(Predicate<BasicObject> filter)
        {
            lock (this)
            {
                var selected = (from obj in this.objects where filter(obj) select obj).ToList();

                foreach (var selobj in selected)
                {
                    this.objects.Remove(selobj);
                    this.index.Remove(selobj.Id);
                }
            }
        }

        public void Update(Predicate<BasicObject> filter, Action<BasicObject> update)
        {
            lock (this)
            {
                int k = 0;
                List<PositionObject> modified = new List<PositionObject>();

                foreach (BasicObject obj in this.objects)
                {
                    if (filter(obj))
                    {
                        BasicObject newobj = obj.MakeDeepCopy();
                        update(newobj);
                        modified.Add(new PositionObject() { Position = k, Object = newobj });
                    }

                    k++;
                }

                foreach (PositionObject pobj in modified)
                    this.objects[pobj.Position] = pobj.Object;
            }
        }

        private class PositionObject
        {
            internal int Position { get; set; }

            internal BasicObject Object { get; set; }
        }
    }
}

