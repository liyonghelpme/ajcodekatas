namespace AjObjects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Collection
    {
        private Dictionary<System.Guid, BasicObject> objects = new Dictionary<Guid, BasicObject>();

        public void Insert(BasicObject obj)
        {
            if (obj["_id"] == null)
                obj["_id"] = Guid.NewGuid();

            Guid id = (Guid) obj["_id"];

            objects[id] = obj.MakeDeepCopy();
        }

        public BasicObject GetObject(Guid id)
        {
            if (!objects.ContainsKey(id))
                return null;

            return objects[id].MakeDeepCopy();
        }
    }
}
