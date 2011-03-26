using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyLibrary.Web.Models;

namespace MyLibrary.Web.Controllers
{
    public class GenreController : Controller
    {
        private IList<Genre> genres;

        public GenreController()
        {
        }

        public GenreController(IList<Genre> genres)
        {
            this.genres = genres;
        }

        public ActionResult Index()
        {
            return View(genres);
        }
    }
}
