using Microsoft.AspNet.Identity.EntityFramework;
using QuizArena.Data.Migrations;
using QuizArena.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizArena.Data
{
    public class QuizArenaDbContext : IdentityDbContext<ApplicationUser>
    {

        public QuizArenaDbContext()
            : this("QuizArenaConnection")
        {
        }

        public QuizArenaDbContext(string nameOfConnectionString)
            : base(nameOfConnectionString)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<QuizArenaDbContext, Configuration>());
        }

        public virtual IDbSet<Category> Categories { get; set; }
        public virtual IDbSet<Question> Questions { get; set; }
        public virtual IDbSet<Quiz> Quizes { get; set; }
        public static QuizArenaDbContext Create()
        {
            return new QuizArenaDbContext();
        }
    }
}
