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
        public UnitOfWork unitOfWork= new UnitOfWork();
        [AllowAnonymous]
        public ActionResult Index()
        {
            var questions =unitOfWork.QuestionRepository.Get();
            var models = new List<QuestionListModel>();
            Mapper.CreateMap<Question, QuestionListModel>().ReverseMap();
            foreach (var q in questions)
            {
                var model = Mapper.Map<Question, QuestionListModel>(q);
                model.OwnerId = q.Owner;
                model.OwnerName = unitOfWork.AccountRepository.GetEntityById(model.OwnerId).Name;
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
            question.Owner = Guid.Parse(HttpContext.User.Identity.Name);
            unitOfWork.QuestionRepository.InsertEntity(question);
            unitOfWork.Save();
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

            var answers=unitOfWork.AnswerRepository.GetEntityById(questionId);

            List<AnswersListModel> models = new List<AnswersListModel>();
            Mapper.CreateMap<Answer, AnswersListModel>();
            foreach (Answer a in question.Answers)
            {
                var answer = Mapper.Map<Answer, AnswersListModel>(a);
                var account = context.Accounts.Find(a.AccountId);
                answer.OwnerName = account.Name;
                models.Add(answer);
            }
            
            return View(models);
        }

        public ActionResult AnswerQuestion()
        {
            return View(new AnswerQuestionModel());
        }

        [HttpPost]
        public ActionResult AnswerQuestion(AnswerQuestionModel model,Guid questionId)
        {
            Mapper.CreateMap<AnswerQuestionModel, Answer>().ReverseMap();
            var answer = Mapper.Map<AnswerQuestionModel, Answer>(model);
            var context = new OverflowVictorContext();
            answer.QuestionId = questionId;
            answer.AccountId = Guid.Parse(HttpContext.User.Identity.Name);
            context.Answers.Add(answer);
            context.SaveChanges();             
            return RedirectToAction("Index", "Question");
        }
    }
}