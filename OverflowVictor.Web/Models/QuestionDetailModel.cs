using System;

namespace OverflowVictor.Web.Models
{
    public class QuestionDetailModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string OwnerEmail { get; set; }
        public int Votes { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificaDate { get; set; }
        public Guid Id { get; set; }
    }
}