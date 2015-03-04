using System;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OverflowVictor.Domain.Entities;

namespace OverflowVictor.Data
{
    public class OverflowVictorContext:DbContext{
        public OverflowVictorContext() : base(ConnectionString.Get())
        {
            
        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; } 
    }

    public static class ConnectionString
    {
        public static string Get()
        {
            var environment = ConfigurationManager.AppSettings["Environment"];
            return String.Format("name={0}", environment);
        }
    }
}
