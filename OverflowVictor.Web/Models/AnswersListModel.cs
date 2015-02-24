using System;

namespace OverflowVictor.Web.Models
{
    public class AnswersListModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Votes { get; set; }
        public Guid AccountId { get; set; }
        public Guid QuestionId { get; set; }
    }
}