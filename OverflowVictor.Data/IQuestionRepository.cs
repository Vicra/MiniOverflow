using System;
using System.Collections.Generic;
using OverflowVictor.Domain.Entities;

namespace OverflowVictor.Data
{
    public interface IQuestionRepository : IDisposable
    {
        IEnumerable<Question> GetQuestions();
        Question GetQuestionById(Guid questionId);
        void InsertQuestion(Question question);
        void DeleteQuestion(Guid questionId);
        void UpdateQuestion(Question question);
        void Save();
    }
}