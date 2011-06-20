namespace MyLibrary.Web.Tests.Controllers
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Web.Mvc;
    using MyLibrary.Web.Models;
    using MyLibrary.Web.Controllers;

    [TestClass]
    public class SubjectControllerTests
    {
        [TestMethod]
        public void GetSubjectsInIndex()
        {
            IList<Subject> subjects = GetSubjects();
            SubjectController controller = new SubjectController(subjects);

            ActionResult result = controller.Index();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));

            ViewResult viewResult = (ViewResult)result;

            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(IList<Subject>));
            Assert.AreSame(subjects, viewResult.ViewData.Model);
        }

        [TestMethod]
        public void GetSubjectInDetail()
        {
            IList<Subject> subjects = GetSubjects();
            SubjectController controller = new SubjectController(subjects);

            ActionResult result = controller.Details(1);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));

            ViewResult viewResult = (ViewResult)result;

            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(Subject));

            Subject model = (Subject)viewResult.ViewData.Model;
            Assert.AreEqual(1, model.Id);
            Assert.AreEqual("Mathematics", model.Name);
        }

        [TestMethod]
        public void GetSubjectForEdit()
        {
            IList<Subject> subjects = GetSubjects();
            SubjectController controller = new SubjectController(subjects);

            ActionResult result = controller.Edit(1);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));

            ViewResult viewResult = (ViewResult)result;

            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(Subject));

            Subject model = (Subject)viewResult.ViewData.Model;
            Assert.AreEqual(1, model.Id);
            Assert.AreEqual("Mathematics", model.Name);
        }

        [TestMethod]
        public void AddSubject()
        {
            IList<Subject> subjects = GetSubjects();
            Subject subject = new Subject() { Name = "Chemistry" };

            SubjectController controller = new SubjectController(subjects);

            ActionResult result = controller.Create(subject);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));

            RedirectToRouteResult redirect = (RedirectToRouteResult)result;
            Assert.IsTrue(string.IsNullOrEmpty(redirect.RouteName));
            Assert.IsTrue(redirect.RouteValues.ContainsKey("id"));
            Assert.AreEqual(subject.Id, redirect.RouteValues["id"]);
            Assert.IsTrue(redirect.RouteValues.ContainsKey("action"));
            Assert.AreEqual("Details", redirect.RouteValues["action"]);

            Assert.IsTrue(subjects.Any(s => s.Name == "Chemistry"));
            Assert.AreEqual(5, subject.Id);
        }

        [TestMethod]
        public void EditSubject()
        {
            IList<Subject> subjects = GetSubjects();
            Subject literature = subjects.Where(s => s.Name == "Literature").FirstOrDefault();
            Subject subject = new Subject() { Name = "SciFi" };

            SubjectController controller = new SubjectController(subjects);

            ActionResult result = controller.Edit(literature.Id, subject);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));

            RedirectToRouteResult redirect = (RedirectToRouteResult)result;
            Assert.IsTrue(string.IsNullOrEmpty(redirect.RouteName));
            Assert.IsTrue(redirect.RouteValues.ContainsKey("id"));
            Assert.AreEqual(literature.Id, redirect.RouteValues["id"]);
            Assert.IsTrue(redirect.RouteValues.ContainsKey("action"));
            Assert.AreEqual("Details", redirect.RouteValues["action"]);

            Assert.IsTrue(subjects.Any(s => s.Name == "SciFi"));
            Assert.AreEqual(literature.Id, subjects.Where(s => s.Name == "SciFi").Single().Id);
        }

        private static IList<Subject> GetSubjects()
        {
            IList<Subject> subjects = new List<Subject>()
            {
                new Subject() { Id = 1, Name = "Mathematics" },
                new Subject() { Id = 2, Name = "Physics" },
                new Subject() { Id = 3, Name = "Biology" },
                new Subject() { Id = 4, Name = "Literature" }
            };

            return subjects;
        }
    }
}
