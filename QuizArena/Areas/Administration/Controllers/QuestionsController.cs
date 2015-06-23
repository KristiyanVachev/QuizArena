using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuizArena.Data;
using QuizArena.Models;

namespace QuizArena.Areas.Administration.Controllers
{
    [Authorize(Roles = "Admin")]
    public class QuestionsController : Controller
    {
        private QuizArenaDbContext db = new QuizArenaDbContext();

        // GET: Administration/Questions
        public ActionResult Index(string sortOrder, string searchString)
        {
            //var questions = db.Questions.Include(q => q.Category);



            //return View(questions.ToList());

            ViewBag.ConditionSortParm = String.IsNullOrEmpty(sortOrder) ? "condition_desc" : "";
            var questions = from s in db.Questions
                            select s;

            //paging
            if (!String.IsNullOrEmpty(searchString))
            {
                questions = questions.Where(s => s.Condition.Contains(searchString));
            }

            //sorting
            switch (sortOrder)
            {
                case "condition_desc":
                    questions = questions.OrderByDescending(s => s.Condition);
                    break;
                default:
                    questions = questions.OrderBy(s => s.Condition);
                    break;
            }
            return View(questions.ToList());
        }

        // GET: Administration/Questions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // GET: Administration/Questions/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            return View();
        }

        // POST: Administration/Questions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Condition,CorrectAnswer,FirstIncorrect,SecondIncorrect,ThirdIncorrect,Description,CategoryId,CorrectsCount,IncorrectsCount")] Question question)
        {
            if (ModelState.IsValid)
            {
                db.Questions.Add(question);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", question.CategoryId);
            return View(question);
        }

        // GET: Administration/Questions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", question.CategoryId);
            return View(question);
        }

        // POST: Administration/Questions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Condition,CorrectAnswer,FirstIncorrect,SecondIncorrect,ThirdIncorrect,Description,CategoryId,CorrectsCount,IncorrectsCount")] Question question)
        {
            if (ModelState.IsValid)
            {
                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", question.CategoryId);
            return View(question);
        }

        // GET: Administration/Questions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: Administration/Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Question question = db.Questions.Find(id);
            db.Questions.Remove(question);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
