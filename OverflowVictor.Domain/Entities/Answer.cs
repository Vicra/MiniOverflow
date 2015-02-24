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
        }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Votes { get; set; }
        public Guid AccountId { get; set; }
        public Guid QuestionId { get; set; }

    }
}
