using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyLibrary.Web.Models;

namespace MyLibrary.Web.Controllers
{
    public class SubjectController : Controller
    {
        private IList<Subject> subjects;

        public SubjectController()
        {
        }

        public SubjectController(IList<Subject> subjects)
        {
            this.subjects = subjects;
        }

        public ActionResult Index()
        {
            return View(subjects);
        }

        public ActionResult Details(int id)
        {
            var model = this.subjects.Where(s => s.Id == id).FirstOrDefault();
            return View(model);
        }

        public ActionResult Create(Subject subject)
        {
            subject.Id = this.subjects.Max(s => s.Id);
            subjects.Add(subject);
            return RedirectToAction("Details", new { id = subject.Id });
        }

        public ActionResult Update(int id, Subject subject)
        {
            Subject toupdate = this.subjects.Where(s => s.Id == id).Single();
            toupdate.Name = subject.Name;
            return RedirectToAction("Details", new { id = id });
        }
    }
}
