using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using OverflowVictor.Domain.Entities;

namespace OverflowVictor.Data
{
    public interface IAnswerRespository : IDisposable
    {
        IEnumerable<Answer> GetAnswers();
        Answer GetAnswerById(Guid answerId);
        void InsertAnswer(Answer answerId);
        void DeleteAnswer(Guid answerId);
        void UpdateAnswer(Answer answer);
        void Save();

    }

    public class AnswerRepository : IDisposable
    {
        private OverflowVictorContext context;

        public AnswerRepository(OverflowVictorContext context)
        {
            this.context = context;
        }
        private IEnumerable<Answer> GetAnswers()
        {
            return context.Answers.ToList();
        }
        public Answer GetAnswerById(int id)
        {
            return context.Answers.Find(id);
        }

        public void InsertAnswer(Answer answer)
        {
            context.Answers.Add(answer);
        }

        public void DeleteAnswer(int answerID)
        {
            Answer answer = context.Answers.Find(answerID);
            context.Answers.Remove(answer);
        }

        public void UpdateAnswer(Answer answer)
        {
            context.Entry(answer).State = EntityState.Modified;
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