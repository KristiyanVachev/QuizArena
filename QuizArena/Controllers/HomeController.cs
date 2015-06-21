using QuizArena.Data;
using QuizArena.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuizArena.Controllers
{
    public class HomeController : Controller
    {
        QuizArenaDbContext context = new QuizArenaDbContext();
        public ActionResult Index()
        {
            //var category = new Category();

            //var questionFromCategory = category.Questions;

            //var denidedQuestions= new List<Question>();

            //var questionsCollection = category.Questions.Where(q => !(denidedQuestions.Contains(q)));

            return View();
        }

        public ActionResult Training()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult FullTest()
        {
            var categories = this.context.Categories.ToList();
            var questions = new List<Question>();
            var counter = 0;

            foreach (var category in categories)
            {
                counter++;
                var questionForAdd = category.Questions.FirstOrDefault(q => q.Id == counter);
                if (questionForAdd != null)
                    questions.Add(questionForAdd);

                counter++;
                questionForAdd = category.Questions.FirstOrDefault(q => q.Id == counter);
                if (questionForAdd != null)
                    questions.Add(questionForAdd);

                counter++;
                questionForAdd = category.Questions.FirstOrDefault(q => q.Id == counter);
                if (questionForAdd != null)
                    questions.Add(questionForAdd);
            }


            ViewBag.Message = "Your application description page.";

            return View(questions);
        }

        public ActionResult Competative()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Statistics()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

    }
}