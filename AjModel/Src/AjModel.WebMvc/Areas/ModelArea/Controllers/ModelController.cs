using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AjModel.WebMvc.ViewModel;

namespace AjModel.WebMvc.Areas.ModelArea.Controllers
{
    public class ModelController : Controller
    {
        private Model model;
        private Context context;

        public ModelController()
            : this(Model.CurrentProvider.GetInstance(), Context.CurrentProvider.GetInstance())
        {
        }

        public ModelController(Model model, Context context)
        {
            this.model = model;
            this.context = context;
        }

        public ActionResult Index(string entity)
        {
            var viewModel = new EntityListViewModel();
            viewModel.Entities = this.context.GetRepository(entity).GetObjects();
            viewModel.EntityModel = this.model.GetEntityModel(entity);
            return View(viewModel);
        }
    }
}
