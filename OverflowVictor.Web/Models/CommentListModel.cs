using System;

namespace OverflowVictor.Web.Models
{
    public class CommentListModel
    {
        public DateTime CreationDate { get; set; }
        public string Description { get; set; }
        public virtual string OwnerName { get; set; }

    }
}