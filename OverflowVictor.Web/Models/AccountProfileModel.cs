using System;
using System.Collections;
using System.Security.AccessControl;

namespace OverflowVictor.Web.Models
{
    public class AccountProfileModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Description { get; set; }
        public int Reputation { get; set; }
        public int QuestionsCount { get; set; }
        public int AnswerCount { get; set; }
        public string RegisterDate { get; set; }
        public int Views { get; set; }
        public string LastSeen { get; set; }
        public IEnumerable Answers { get; set; }
        public IEnumerable Questions { get; set; }

    }
}