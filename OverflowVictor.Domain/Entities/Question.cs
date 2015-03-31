
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverflowVictor.Domain.Entities
{
    public class Question:IEntity
    {
        public Guid Id { get; private set; }

        public Question()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.Now;
            ModificationDate = DateTime.Now;
            HasCorrectAnswer = false;
        }
        
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Vote> Voters { get; set; }
        public int Votes { get; set; }
        public string Title { get; set; }   
        public string Description { get; set; }
        public Guid Owner { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public bool HasCorrectAnswer { get; set; }
        public int Views { get; set; }
        public int AnswerCount { get; set; }


    }
}
