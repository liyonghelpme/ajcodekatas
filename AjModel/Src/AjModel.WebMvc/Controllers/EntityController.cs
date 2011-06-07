using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AjModel.WebMvc.ViewModel;

namespace AjModel.WebMvc.Controllers
{
    public class EntityController : Controller
    {
        public ActionResult Index()
        {
            var model = new EntityListViewModel();
            return View(model);
        }
    }
}
