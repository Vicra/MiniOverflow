using System;

namespace OverflowVictor.Web.Models
{
    public class QuestionListModel
    {
        public string Title { get; set; }
        public int Votes { get; set; }
        public string OwnerName { get; set; }

        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public int AnswerCount { get; set; }
        public int Views { get; set; }
        public string Date { get; set; }


    }
}