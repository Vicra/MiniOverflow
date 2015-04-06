using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.DynamicData;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using OverflowVictor.Data;
using OverflowVictor.Domain.Entities;
using OverflowVictor.Web.MashUps;
using OverflowVictor.Web.Models;

namespace OverflowVictor.Web.Controllers
{
    [Authorize]
    public class QuestionController : Controller
    {
        public UnitOfWork unitOfWork = new UnitOfWork();

        [AllowAnonymous]
        public ActionResult Index()
        {
            var questions = unitOfWork.QuestionRepository.Get();
            var models = new List<QuestionListModel>();
            Mapper.CreateMap<Question, QuestionListModel>().ReverseMap();
            TimeCalculator calculator=new TimeCalculator();
            foreach (var q in questions)
            {
                //var date=calculator.GetTime(q.CreationDate);
                var date = calculator.GetTime(q.CreationDate);
                var model = Mapper.Map<Question, QuestionListModel>(q);
                model.Date = date;
                model.OwnerId = q.Owner;
                model.OwnerName = unitOfWork.AccountRepository.GetById(model.OwnerId).Name;
                model.LastName = unitOfWork.AccountRepository.GetById(model.OwnerId).LastName;
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
            var question = Mapper.Map<AskQuestionModel, Question>(model);
            question.Owner = Guid.Parse(HttpContext.User.Identity.Name);
            unitOfWork.QuestionRepository.Insert(question);
            unitOfWork.Save();
            return RedirectToAction("Index", "Question");
        }

        [AllowAnonymous]
        public ActionResult QuestionDetail(Guid questionId)
        {
            Mapper.CreateMap<Question, QuestionDetailModel>();
            var question = unitOfWork.QuestionRepository.GetById(questionId);
            question.Views += 1;
            var owner = unitOfWork.AccountRepository.GetById(question.Owner);
            var model = Mapper.Map<Question, QuestionDetailModel>(question);
            TimeCalculator calculator=new TimeCalculator();
            string date = calculator.GetTime(question.CreationDate);
            model.Date = date;
            unitOfWork.QuestionRepository.Update(question);
            unitOfWork.Save();
            model.OwnerEmail = owner.Email;
            model.Name = owner.Name;
            model.LastName =owner.LastName;
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult AnswerList(Guid questionId)
        {
            var quest = unitOfWork.QuestionRepository.GetById(questionId);
            List<AnswersListModel> models = new List<AnswersListModel>();
            TimeCalculator calculator=new TimeCalculator();
            Mapper.CreateMap<Answer, AnswersListModel>();
            foreach (Answer a in quest.Answers)
            {
                var answer = Mapper.Map<Answer, AnswersListModel>(a);
                answer.OwnerName = unitOfWork.AccountRepository.GetById(answer.AccountId).Name;
                answer.LastName = unitOfWork.AccountRepository.GetById(answer.AccountId).LastName;
                var account = unitOfWork.AccountRepository.GetById(a.AccountId);
                answer.OwnerName = account.Name;
                answer.Date = calculator.GetTime(a.CreationDate);
                models.Add(answer);
            }
            return View(models);
        }

        public ActionResult AnswerQuestion()
        {
            return View(new AnswerQuestionModel());
        }

        [HttpPost]
        public ActionResult AnswerQuestion(AnswerQuestionModel model, Guid questionId)
        {
            Mapper.CreateMap<AnswerQuestionModel, Answer>().ReverseMap();
            var answer = Mapper.Map<AnswerQuestionModel, Answer>(model);
            answer.QuestionId = questionId;
            var question = unitOfWork.QuestionRepository.GetById(questionId);
            question.AnswerCount += 1;

            answer.AccountId = Guid.Parse(HttpContext.User.Identity.Name);
            unitOfWork.QuestionRepository.Update(question);
            unitOfWork.AnswerRepository.Insert(answer);
            unitOfWork.Save();
            return RedirectToAction("Index", "Question");
        }
        public ActionResult VoteUpQuestion(Guid questId)
        {
            var ownerId = Guid.Parse(HttpContext.User.Identity.Name);

            var question = unitOfWork.QuestionRepository.GetById(questId);
            question.Votes += 1;
            unitOfWork.QuestionRepository.Update(question);
            unitOfWork.Save();
            return RedirectToAction("QuestionDetail", "Question", new { questionId = questId });
            
        }

        public ActionResult VoteDownQuestion(Guid questId)
        {
            var question = unitOfWork.QuestionRepository.GetById(questId);
            question.Votes -= 1;
            unitOfWork.QuestionRepository.Update(question);
            unitOfWork.Save();
            return RedirectToAction("QuestionDetail", "Question", new { questionId = questId });
        }

        public ActionResult VoteUpAnswer(Guid answerId)
        {
            var answer = unitOfWork.AnswerRepository.GetById(answerId);
            answer.Votes += 1;
            unitOfWork.AnswerRepository.Update(answer);
            unitOfWork.Save();
            return RedirectToAction("QuestionDetail", "Question", new { questionId = answer.QuestionId });
        }
        
        public ActionResult VoteDownAnswer(Guid answerId)
        {
            var answer = unitOfWork.AnswerRepository.GetById(answerId);
            answer.Votes -= 1;
            unitOfWork.AnswerRepository.Update(answer);
            unitOfWork.Save();
            return RedirectToAction("QuestionDetail", "Question", new { questionId = answer.QuestionId });

        }

        
        public ActionResult PressCorrect(AnswersListModel model)
        {
            var question = unitOfWork.QuestionRepository.GetById(model.QuestionId);
            var userId = Guid.Parse(HttpContext.User.Identity.Name);
            if (model.AccountId == userId)
            {
                    if (model.Correct)
                    {
                        question.HasCorrectAnswer = false;
                        UnMark(model);
                    }
                    else if (!model.Correct)
                    {
                        Mark(model);
                        question.HasCorrectAnswer = true;
                    }
                    unitOfWork.QuestionRepository.Update(question);
                    unitOfWork.Save();
            }
            return RedirectToAction("QuestionDetail", "Question", new { questionId = model.QuestionId });
        }

        private void Mark(AnswersListModel model)
        {
            Mapper.CreateMap<AnswersListModel, Answer>();
            var answer = Mapper.Map<AnswersListModel, Answer>(model);
            answer.Correct = true;
            unitOfWork.AnswerRepository.Update(answer);
        }

        private void UnMark(AnswersListModel model)
        {
            Mapper.CreateMap<AnswersListModel, Answer>();
            var answer = Mapper.Map<AnswersListModel, Answer>(model);
            answer.Correct = false;
            unitOfWork.AnswerRepository.Update(answer);
        }
    }
}