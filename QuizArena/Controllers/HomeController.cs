using QuizArena.Data;
using QuizArena.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace QuizArena.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        QuizArenaDbContext context = new QuizArenaDbContext();
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Training()
        {
            //PSEUDO CODE
            //like the FUllGame but...
            //get the categoryId's of the 2 worst categories
            //put 30% of the worst, 20% of the 2nd worst, 50% each category - 20questions

            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult FullTest()
        {
            //starting a new quiz
            var userId = this.User.Identity.GetUserId();
            var quiz = new Quiz
            {
                ApplicationUserId = userId,
                GameType = "Full Game",
                Start = DateTime.Now,
                CorrectCount = 0,
                InCorrectCount = 0
            };
            this.context.Quizes.Add(quiz);
            this.context.SaveChanges();

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
                    int random = rnd.Next(0, numberOfQuestions);
                    if (indexes.Contains(random))
                    {
                        i--;
                    }
                    else
                    {
                        indexes.Add(random);
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

        //public ActionResult CorrectAnswer(int id, string gameType)
        //{
        //    var userId = this.User.Identity.GetUserId();
        //    var user = this.context.Users.FirstOrDefault(us => us.Id == userId);
        //    var quiz = this.context.Quizes.OrderByDescending(q => q.Start).FirstOrDefault(q => q.ApplicationUserId == userId);
        //    var questions = this.context.Questions.FirstOrDefault(q => q.Id == id);

        //    if (gameType == "FullTest")
        //    {
        //        quiz.CorrectCount++;
        //        if (questions.CorrectsCount == null)
        //        {
        //            questions.CorrectsCount = 1;
        //        }
        //        else
        //        {
        //            questions.CorrectsCount++;
        //        }
        //    }
        //    else //while Competative is not yet active, the only other option is Training.
        //    {
        //        quiz.CorrectAnswered.Add(context.Questions.FirstOrDefault(q => q.Id == id));
        //    }

        //    context.SaveChanges();
        //    return new EmptyResult();
        //}

        //public ActionResult InCorrectAnswer(int id, string gameType)
        //{
        //    var userId = this.User.Identity.GetUserId();
        //    var user = this.context.Users.FirstOrDefault(us => us.Id == userId);
        //    var quiz = this.context.Quizes.OrderByDescending(q => q.Start).FirstOrDefault(q => q.ApplicationUserId == userId);
        //    var questions = this.context.Questions.FirstOrDefault(q => q.Id == id);

        //    if (gameType == "FullTest")
        //    {
        //        quiz.InCorrectCount++;
        //        if (questions.IncorrectsCount == null)
        //        {
        //            questions.IncorrectsCount = 1;
        //        }
        //        else
        //        {
        //            questions.IncorrectsCount++;
        //        }
        //    }
        //    else //while Competative is not yet active, the only other option is Training.
        //    {
        //        quiz.InCorrectAnswered.Add(context.Questions.FirstOrDefault(q => q.Id == id));
        //    }
        //    context.SaveChanges();
        //    return new EmptyResult();
        //}

        //public ActionResult EndGame(int id, string gameType)
        //{
        //    var userId = this.User.Identity.GetUserId();
        //    var user = this.context.Users.FirstOrDefault(us => us.Id == userId);
        //    var quiz = this.context.Quizes.OrderByDescending(q => q.Start).FirstOrDefault(q => q.ApplicationUserId == userId);

        //    quiz.End = DateTime.Now;
        //    //add points, increase level.

        //    if (gameType == "FullTest")
        //    {
        //        //return quiz - time and counts
        //    }
        //    else //training
        //    {
        //        foreach (var question in quiz.CorrectAnswered)
        //        {
        //            //question.Category.Difficulty++;

        //            //user.CategoryExp.Difficulty++; //RIGHT!??
        //        }
        //        foreach (var question in quiz.InCorrectAnswered)
        //        {
        //            //question.Category.Difficulty--;
        //        }

        //        //return quiz - wrongs(condition, correct, description, number of wrongs)
        //    }

        //    return new EmptyResult();
        //}

        public ActionResult EndFullGame(int correctCount, int inCorrectCount)
        {
            var userId = this.User.Identity.GetUserId();
            var user = this.context.Users.FirstOrDefault(us => us.Id == userId);
            var quiz = this.context.Quizes.OrderByDescending(q => q.Start).FirstOrDefault(q => q.ApplicationUserId == userId);

            quiz.End = DateTime.Now;
            //add points, increase level.
            quiz.CorrectCount = correctCount;
            quiz.InCorrectCount = inCorrectCount;
            context.SaveChanges();

            ViewBag.gameTime = quiz.End - quiz.Start;
            ViewBag.correctCount = quiz.CorrectCount;
            ViewBag.inCorrectCount = quiz.InCorrectCount;

            //ViewBag.procents = 
            // return RedirectToAction("Imeto na action-a");
            return PartialView("_EndFullGame"); //view, което показва резултата за този вид игра..
        }

    }
}