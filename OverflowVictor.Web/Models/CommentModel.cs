using System;

namespace OverflowVictor.Web.Models
{
    public class CommentModel
    {
        public Guid Id { get; set; }
        public Guid FatherId { get; set; }//puede ser un question o answer id
        public string Comment { get; set; }
        public string OwnerName { get; set; }
        public DateTime CreationTime { get; set; }

    }
}