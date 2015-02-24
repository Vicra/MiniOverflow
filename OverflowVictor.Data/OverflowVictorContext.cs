using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OverflowVictor.Domain.Entities;

namespace OverflowVictor.Data
{
    public class OverflowVictorContext:DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Question> Questions { get; set; }        
    }
}
