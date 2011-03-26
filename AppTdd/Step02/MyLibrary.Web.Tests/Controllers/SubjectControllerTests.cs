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
            IEnumerable<Subject> subjects = GetSubjects();
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
            IEnumerable<Subject> subjects = GetSubjects();
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

        private static IEnumerable<Subject> GetSubjects()
        {
            IEnumerable<Subject> subjects = new List<Subject>()
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
