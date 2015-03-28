using System;

namespace OverflowVictor.Web.Models
{
    public class QuestionDetailModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string OwnerEmail { get; set; }
        public Guid OwnerId { get; set; }
        public int Votes { get; set; }
        public Guid Id { get; set; }
        public string Date { get; set; }
        public int Views { get; set; }

    }
}