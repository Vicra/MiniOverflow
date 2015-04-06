using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverflowVictor.Domain.Entities
{
    public class Answer : IEntity
    {
        public Guid Id { get; set; }

        public Answer()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.Now;
        }
        public string Description { get; set; }
        public int Votes { get; set; }
        public Guid AccountId { get; set; }
        public Guid QuestionId { get; set; }
        public bool Correct { get; set; }
        public int Views { get; set; }
        public DateTime CreationDate { get; set; }

        
        public ICollection<Comment> Comments{ get; set; }
        
    }
}
