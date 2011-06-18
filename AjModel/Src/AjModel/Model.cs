using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AjModel
{
    public class Model
    {
        private IList<EntityModel> entityModels = new List<EntityModel>();

        public Model()
        {
        }

        public IEnumerable<EntityModel> EntityModels
        {
            get
            {
                return this.entityModels;
            }
        }

        public void AddEntityModel(EntityModel entityModel)
        {
            this.entityModels.Add(entityModel);
        }

        public EntityModel GetEntityModel(string name)
        {
            return this.entityModels.Where(em => em.Name == name).FirstOrDefault();
        }

        public EntityModel GetEntityModel<T>()
        {
            return this.entityModels.Where(em => em.Type == typeof(T)).FirstOrDefault();
        }
    }
}

