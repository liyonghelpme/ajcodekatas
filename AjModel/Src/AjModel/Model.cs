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

        public Model(Type domain)
        {
            foreach (Type type in GetListTypes(domain)) 
            {
                Type model = typeof(EntityModel<>);
                this.entityModels.Add((EntityModel) Activator.CreateInstance(model.MakeGenericType(type)));
            }
        }

        public static IModelProvider CurrentProvider { get; set; }

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

        public EntityModel<T> GetEntityModel<T>() where T : new()
        {
            return (EntityModel<T>) this.entityModels.Where(em => em.Type == typeof(T)).FirstOrDefault();
        }

        private static IEnumerable<Type> GetListTypes(Type type)
        {
            foreach (var property in type.GetProperties())
            {
                if (property.PropertyType.IsInterface && property.PropertyType.IsGenericType)
                {
                    var types = property.PropertyType.GetGenericArguments();
                    if (types.Length != 1)
                        break;
                    yield return types[0];
                    continue;
                }

                foreach (var propertyInterface in property.PropertyType.GetInterfaces())
                {
                    if (!propertyInterface.IsGenericType)
                        continue;

                    if (propertyInterface.Name != "IList`1")
                        continue;

                    var types = propertyInterface.GetGenericArguments();

                    if (types.Length != 1)
                        continue;

                    yield return types[0];
                }
            }
        }
    }
}

