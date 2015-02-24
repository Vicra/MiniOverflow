
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
            ModificaDate = DateTime.Now;
        }

        public virtual List<Answer> Answers { get; set; }
        public int Votes { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid Owner { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificaDate { get; set; }
    }
}
