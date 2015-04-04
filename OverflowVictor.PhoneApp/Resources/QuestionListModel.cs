using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverflowVictor.PhoneApp.Resources
{
    public class QuestionListModel
    {
        public string Title { get; set; }
        public int Votes { get; set; }
        public int Views { get; set; }
        public int Answers { get; set; }
        public DateTime CreationDate { get; set; }
        public string OwnerName { get; set; }

        public Guid OwnerID { get; set; }
        public Guid QuestionID { get; set; }
        public string ImageUrl { get; set; }
    }
}
