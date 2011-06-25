using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AjModel.WebMvc.Models
{
    public class ModelProvider : IModelProvider
    {
        private static Model instance;

        static ModelProvider()
        {
            instance = new Model(typeof(Domain));
        }

        public static Model Instance { get { return instance; } }

        public Model GetInstance()
        {
            return instance;
        }
    }
}

