using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverflowVictor.Domain.Entities
{
    public class QuestionComment:IEntity
    {
        public Guid Id { get; set; }

        public QuestionComment() { Id = Guid.NewGuid(); }
        public Guid AccountId { get; set; }
        public Guid QuestionId { get; set; }
    }
}
