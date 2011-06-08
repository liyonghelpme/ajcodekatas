using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AjModel.WebMvc.ViewModel;
using AjModel.WebMvc.Models;

namespace AjModel.WebMvc.Controllers
{
    public class EntityController : Controller
    {
        public ActionResult Index()
        {
            var model = new EntityListViewModel();
            model.Entities = Domain.Instance.Customers;
            model.EntityModel = new EntityModel(typeof(Customer));
            return View(model);
        }
    }
}
