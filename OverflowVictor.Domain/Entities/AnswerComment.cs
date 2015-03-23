using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverflowVictor.Domain.Entities
{
    public class AnswerComment
    {
        public Guid Id { get; set; }

        public AnswerComment() { Id = Guid.NewGuid(); }
        public Guid AccountId { get; set; }
        public Guid AnswerrId{ get; set; }
    }
}
