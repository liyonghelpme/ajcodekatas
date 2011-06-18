using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AjModel.Fluent
{
    public static class ModelExtensions
    {
        public static FluentEntityModel<T> ForEntity<T>(this Model model)
        {
            var entityModel = model.GetEntityModel<T>();

            if (entityModel == null)
            {
                entityModel = new EntityModel<T>();
                model.AddEntityModel(entityModel);
            }

            return new FluentEntityModel<T>(entityModel);
        }
    }
}
