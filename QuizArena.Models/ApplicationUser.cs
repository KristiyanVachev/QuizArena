using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace QuizArena.Models
{
    public class ApplicationUser : IdentityUser
    {
        private ICollection<Quiz> quizes;

        public ApplicationUser()
        {
            this.quizes = new HashSet<Quiz>();
        }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public virtual ICollection<Quiz> Quizes
        {
            get
            {
                return this.quizes;
            }
            set
            {
                this.quizes = value;
            }
        }

        public int Points { get; set; }
        public int Level { get; set; }
        public ICollection<Category> CategoryExp { get; set; }

    }
}
