using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using OverflowVictor.Data;
using OverflowVictor.Domain.Entities;
using OverflowVictor.Web.Models;

namespace OverflowVictor.Web.Controllers
{
    [Authorize]
    public class QuestionController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            var models= new List<QuestionListModel>();
            Mapper.CreateMap<Question, QuestionListModel>().ReverseMap();
            var context = new OverflowVictorContext();
            foreach (var q in context.Questions)
            {
                var model = Mapper.Map<Question, QuestionListModel>(q);
                var owner = context.Accounts.Find(q.Owner);
                model.OwnerName = owner.Name;
                model.OwnerID = owner.Id;
                models.Add(model);
            }
            return View(models);
        }

        public ActionResult AskQuestion()
        {
            return View(new AskQuestionModel());
        }

        [HttpPost]
        public ActionResult AskQuestion(AskQuestionModel model)
        {
            Mapper.CreateMap<AskQuestionModel, Question>().ReverseMap();
            var question =Mapper.Map<AskQuestionModel, Question>(model);
            var context = new OverflowVictorContext();
            question.Owner = Guid.Parse(HttpContext.User.Identity.Name);
            context.Questions.Add(question);
            context.SaveChanges();
            return RedirectToAction("Index","Question");

        }

        public ActionResult QuestionDetail(Guid questionId)
        {
            Mapper.CreateMap<Question, QuestionDetailModel>();
            var context = new OverflowVictorContext();
            var question = context.Questions.Find(questionId);
            var onw = context.Accounts.Find(question.Owner);
            var model = Mapper.Map<Question, QuestionDetailModel>(question);
            model.OwnerEmail = onw.Email;
            return View(model);

        }

        public ActionResult AnswerList(Guid questionId)
        {
            var context = new OverflowVictorContext();
            var question = context.Questions.Find(questionId);
            List<AnswersListModel> models = new List<AnswersListModel>();
            Mapper.CreateMap<Answer, AnswersListModel>();
            foreach (Answer a in question.Answers)
            {
                var answer = Mapper.Map<Answer, AnswersListModel>(a);
                models.Add(answer);
            }
            
            return View(models);
        }

        public ActionResult AnswerQuestion()
        {
            return View(new AnswerQuestionModel());
        }

        [HttpPost]
        public ActionResult AnswerQuestion(AnswerQuestionModel model)
        {
            Mapper.CreateMap<AnswerQuestionModel, Answer>().ReverseMap();
            var answer = Mapper.Map<AnswerQuestionModel, Answer>(model);
            var context = new OverflowVictorContext();

            /*
            answer.Id = Guid.Parse(HttpContext.User.Identity.Name);
            context.Questions.Add(answer);
            context.SaveChanges();
             */
             
            return RedirectToAction("Index", "Question");
        }
    }
}