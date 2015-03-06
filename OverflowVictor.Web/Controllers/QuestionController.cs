﻿using System;
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
                model.OwnerName = unitOfWork.AccountRepository.GetById(model.OwnerId).Name;
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
            unitOfWork.QuestionRepository.Insert(question);
            unitOfWork.Save();
            return RedirectToAction("Index","Question");
        }
        [AllowAnonymous]
        public ActionResult QuestionDetail(Guid questionId)
        {
            Mapper.CreateMap<Question, QuestionDetailModel>();
            var question = unitOfWork.QuestionRepository.GetById(questionId);
            var owner = unitOfWork.AccountRepository.GetById(question.Owner);
            var model = Mapper.Map<Question, QuestionDetailModel>(question);
            model.OwnerEmail = owner.Email;
            return View(model);
        }
        [AllowAnonymous]
        public ActionResult AnswerList(Guid questionId)
        {
            var quest = unitOfWork.QuestionRepository.GetById(questionId);
            List<AnswersListModel> models = new List<AnswersListModel>();
            Mapper.CreateMap<Answer, AnswersListModel>();
            foreach (Answer a in quest.Answers)
            {
                var answer = Mapper.Map<Answer, AnswersListModel>(a);
                var account = unitOfWork.AccountRepository.GetById(a.AccountId);
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
            answer.QuestionId = questionId;
            answer.AccountId = Guid.Parse(HttpContext.User.Identity.Name);
            unitOfWork.AnswerRepository.Insert(answer);
            unitOfWork.Save();            
            return RedirectToAction("Index", "Question");
        }
    }
}