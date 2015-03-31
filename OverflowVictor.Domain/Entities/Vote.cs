using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverflowVictor.Domain.Entities
{
    public class Vote
    {
        public Vote()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public virtual Account Voter { get; set; }
        public int Value;
        public virtual Question Question { get; set; }
        public virtual Answer Answer { get; set; }

        
    }
}
