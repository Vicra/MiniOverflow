using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverflowVictor.Domain.Entities
{
    public class Comment:IEntity
    {
        public Comment()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.Now;
        }
        public Guid Id { get; private set; }
        public DateTime CreationDate { get; set; }
        public string Description { get; set; }
        public virtual Account Account { get; set; }
        public Question Question { get; set; }
        public Answer Answer{ get; set; }
    }
}
