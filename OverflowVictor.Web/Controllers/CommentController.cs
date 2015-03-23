using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OverflowVictor.Data;
using OverflowVictor.Web.Models;

namespace OverflowVictor.Web.Controllers
{
    public class CommentController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        public ActionResult CommentAnswer(CommentModel model)
        {
            List<CommentModel> commentAnswers = new List<CommentModel>();
            return View(commentAnswers);
        }

        public ActionResult CommentQuestion(CommentModel model )
        {
            return View(model);
        }
	}
}