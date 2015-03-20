using System.ComponentModel.DataAnnotations;

namespace OverflowVictor.Web.Models
{
    public class AnswerQuestionModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
    }
}