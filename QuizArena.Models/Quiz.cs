using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizArena.Models
{
    public class Quiz
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        //public virtual ApplicationUser ApplicationUser { get; set; }
        public string  GameType { get; set; }
        //public DateTime Start { get; set; }
        //public DateTime End { get; set; }
        public int CorrectCount { get; set; }
        public int InCorrectCount { get; set; }
        public virtual ICollection<Question> CorrectAnswered { get; set; }
        public virtual ICollection<Question> InCorrectAnswered { get; set; }
    } 
}
