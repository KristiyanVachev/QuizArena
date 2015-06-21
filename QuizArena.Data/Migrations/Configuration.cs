namespace QuizArena.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using QuizArena.Data;
    using QuizArena.Models;
    using System.Collections.Generic;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity;

    internal sealed class Configuration : DbMigrationsConfiguration<QuizArenaDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "QuizArena.Data.QuizArenaDbContext";
        }

        protected override void Seed(QuizArenaDbContext context)
        {
            //fucking live saver!
            if(context.Categories.Any())
            {
                return;
            }

            context.Categories.AddOrUpdate(
                c => c.Id,
                //new Category
                //{
                //    Name = "Software",
                //    Questions = new List<Question>()
                //    {
                //        new Question
                //        {
                //            Condition = "Which is software?",
                //            CorrectAnswer = {Text = "Antivirus"},
                //            InCorrectAnswers = new List<Answer>()
                //                {
                //                    new Answer { Text = "RAM"},
                //                    new Answer { Text = "Hard Drive"},
                //                    new Answer { Text = "Graphics card"}
                //                }
                //        },
                //    }
                //}
                new Category { Name = "Hardware", Id = 0 },
                new Category { Name = "Software", Id = 1 }
                );

            context.Questions.AddOrUpdate(
                q => q.Id,
                new Question
                {
                    Condition = "Which is software?",
                    CorrectAnswer = "Antivirus",
                    FirstIncorrect = "RAM",
                    SecondIncorrect = "Hard disk",
                    ThirdIncorrect = "Processor",
                    CategoryId = 1
                }
                );
            
     
            //----------
            //ROLES (Admin)
            const string roleName = "Admin";

            var userRole = new IdentityRole
            {
                Name = roleName,
                Id = Guid.NewGuid().ToString()
            };

            context.Roles.AddOrUpdate(userRole);

            var hasher = new PasswordHasher();

            var user = new ApplicationUser
            {
                UserName = "Admin",
                PasswordHash = hasher.HashPassword("123456"),
                Email = "Admin@abv.bg",
                SecurityStamp = Guid.NewGuid().ToString()
            };

            user.Roles.Add(new IdentityUserRole
            {
                RoleId = userRole.Id,
                UserId = user.Id
            });

            context.Users.AddOrUpdate(user);



            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
