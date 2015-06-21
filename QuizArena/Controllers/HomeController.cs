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
            Random rnd = new Random();
            var categories = this.context.Categories.ToList();
            var questions = new List<Question>();
            var indexes = new List<int>(); //list for the three question indexes i need

            //Taking questions from each category.
            foreach (var category in categories)
            {
                indexes.Clear();

                var numberOfQuestions = category.Questions.Count(); //amount of the questions in the category

                var questionsNeeded = 3;
                if (numberOfQuestions < 3) //validation if the amount of questions is not enough
                {
                    questionsNeeded = numberOfQuestions;
                }

                //getting the amount of needed indexes for the needed questions
                for (int i = 0; i < questionsNeeded; i++)
                {
                    if (indexes.Contains(rnd.Next(0, numberOfQuestions)))
                    {
                        i--;                    
                    }
                    else
                    {
                        indexes.Add(rnd.Next(0, numberOfQuestions));
                    }
                }

                //Iterating trough every question in the category and picking the "questions needed"
                int counter = 0;
                foreach (var question in category.Questions)
                {
                    foreach (var index in indexes)
                    {
                        if (counter == index)
                        {
                            questions.Add(question);
                            questionsNeeded--;
                            //--TO-DO-- maybe remove the index for optimization
                            continue;
                        }
                    }

                    if (questionsNeeded < 1) //when it doesn't need any more questions, stop.
                    {
                        break;
                    }

                    counter++;
                }
            }

            ViewBag.Message = "Your application description page.";

            return View(questions);
        }

        //public ActionResult CorrectAnswer(int id)
        //{
        //    // Mojesh da dobavish tochki na user-a
        //    // Mojesh da dobavish vuprosa v nqkakva kolekciq, za da ne moje da bude zadavan sled tova otnovo

        //    return new EmptyResult();
        //}

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