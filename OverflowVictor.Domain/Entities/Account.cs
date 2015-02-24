using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverflowVictor.Domain.Entities
{
    public class Account : IEntity
    {
        public Guid Id
        {
            get;
            private set;

        }

        public Account()
        {
            Id = Guid.NewGuid();
        }

        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
