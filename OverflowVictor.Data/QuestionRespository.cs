using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using OverflowVictor.Domain.Entities;

namespace OverflowVictor.Data
{
    public class QuestionRespository : IQuestionRepository, IDisposable
    {
        private OverflowVictorContext context;

        public QuestionRespository(OverflowVictorContext context)
        {
            this.context = context;
        }

        public IEnumerable<Question> GetQuestions()
        {
            return context.Questions.ToList();
        }

        public Question GetQuestionById(Guid id)
        {
            return context.Questions.Find(id);
        }

        public void InsertQuestion(Question question)
        {
            context.Questions.Add(question);
        }

        public void DeleteQuestion(Guid questionId)
        {
            Question question = context.Questions.Find(questionId);
            context.Questions.Remove(question);
        }

        public void UpdateQuestion(Question question)
        {
            context.Entry(question).State = EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}