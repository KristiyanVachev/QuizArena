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
        public ActionResult Index()
        {
            //var category = new Category();

            //var questionFromCategory = category.Questions;

            //var denidedQuestions= new List<Question>();

            //var questionsCollection = category.Questions.Where(q => !(denidedQuestions.Contains(q)));

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}