
using System;
using System.Security.Principal;
using System.Threading;
using OverflowVictor.Domain.Entities;

namespace OverflowVictor.Data
{
    public class UnitOfWork
    {
        private OverflowVictorContext context = new OverflowVictorContext();
        private Repository<Account> accountRepository;
        private Repository<Question> questionRepository;
        private Repository<Answer> answerRepository;
        private Repository<Comment> commentRepository;
        

        public Repository<Account> AccountRepository
        {
            get
            {

                if (this.accountRepository == null)
                {
                    this.accountRepository = new Repository<Account>(context);
                }
                return accountRepository;
            }
        }

        public Repository<Question> QuestionRepository
        {
            get
            {

                if (this.questionRepository == null)
                {
                    this.questionRepository = new Repository<Question>(context);
                }
                return questionRepository;
            }
        }

        public Repository<Answer> AnswerRepository
        {
            get
            {

                if (this.answerRepository == null)
                {
                    this.answerRepository = new Repository<Answer>(context);
                }
                return answerRepository;
            }
        }
        public Repository<Comment> CommentRepository
        {
            get
            {

                if (this.commentRepository== null)
                {
                    this.commentRepository = new Repository<Comment>(context);
                }
                return commentRepository;
            }
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