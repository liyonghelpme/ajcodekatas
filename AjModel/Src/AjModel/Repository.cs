using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace AjModel
{
    public abstract class Repository
    {
        public Repository(EntityModel model)
        {
            this.EntityModel = model;
        }

        public EntityModel EntityModel { get; set; }

        public abstract void AddObject(object entity);

        public abstract void RemoveObject(object entity);

        public abstract object GetObject(object id);

        public abstract IEnumerable GetObjects();
    }

    public class Repository<T> : Repository where T : new()
    {
        private IList<T> entities;

        public Repository(EntityModel<T> model, IList<T> entities)
            : base(model)
        {
            this.entities = entities;
        }

        public override void AddObject(object entity)
        {
            this.AddEntity((T)entity);
        }

        public void AddEntity(T entity)
        {
            this.entities.Add(entity);
        }

        public override void RemoveObject(object entity)
        {
            this.RemoveEntity((T) entity);
        }

        public void RemoveEntity(T entity)
        {
            this.entities.Remove(entity);
        }

        public override IEnumerable GetObjects()
        {
            return this.GetEntities();
        }

        public IEnumerable<T> GetEntities()
        {
            return (IEnumerable<T>) this.entities;
        }

        public override object GetObject(object id)
        {
            return this.GetEntity(id);
        }

        public T GetEntity(object id)
        {
            PropertyModel prop = this.EntityModel.Properties.First();

            return this.entities.Where(e => id.Equals(prop.GetValue(e))).FirstOrDefault();
        }
    }
}
